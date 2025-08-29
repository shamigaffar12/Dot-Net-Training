using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class BillReceipt : System.Web.UI.Page
    {
        protected int paymentId = 0;
        protected string htmlContent = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["paymentId"], out paymentId) || paymentId <= 0)
            {
                string consumerNumber = Request.QueryString["consumerNumber"];
                if (!string.IsNullOrEmpty(consumerNumber))
                {
                    paymentId = GetLatestPaymentIdByConsumerNumber(consumerNumber);
                    if (paymentId <= 0)
                    {
                        litReceipt.Text = "<div class='alert alert-danger'>No payments found for the given consumer number.</div>";
                        btnDownloadPdf.Visible = false;
                        return;
                    }
                }
                else
                {
                    litReceipt.Text = "<div class='alert alert-danger'>Invalid payment ID.</div>";
                    btnDownloadPdf.Visible = false;
                    return;
                }
            }

            if (!IsPostBack)
            {
                LoadReceiptDetails(paymentId);
            }
        }

        private int GetLatestPaymentIdByConsumerNumber(string consumerNumber)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 payment_id FROM Payments WHERE consumer_number = @consumerNumber ORDER BY payment_date DESC", con);
                cmd.Parameters.AddWithValue("@consumerNumber", consumerNumber);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void LoadReceiptDetails(int paymentId)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT p.payment_id, p.consumer_number, p.amount, p.payment_date, p.method, p.status, p.txn_ref,
                           c.name as user_name, c.phone, c.email, c.address,
                           e.consumer_name, e.units_consumed, e.bill_amount, e.bill_date
                    FROM Payments p
                    LEFT JOIN Connections c ON c.consumer_number = p.consumer_number
                    LEFT JOIN ElectricityBill e ON e.consumer_number = p.consumer_number
                    WHERE p.payment_id = @pid", con);

                cmd.Parameters.AddWithValue("@pid", paymentId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        litReceipt.Text = $@"
<div class='row'>
    <div class='col-md-12'>
        <h5 class='text-primary'>JBVNL Electricity Bill Payment Receipt</h5>
        <hr />
        <p><strong>Consumer Number:</strong> {dr["consumer_number"]}</p>
        <p><strong>Consumer Name:</strong> {dr["consumer_name"]}</p>
        <p><strong>Customer Name:</strong> {dr["user_name"]}</p>
        <p><strong>Phone:</strong> {dr["phone"]}</p>
        <p><strong>Email:</strong> {dr["email"]}</p>
        <p><strong>Address:</strong> {dr["address"]}</p>
        <hr />
        <p><strong>Bill Date:</strong> {Convert.ToDateTime(dr["bill_date"]):yyyy-MM-dd}</p>
        <p><strong>Units Consumed:</strong> {dr["units_consumed"]} kWh</p>
        <p><strong>Bill Amount:</strong> Rs. {Convert.ToDouble(dr["bill_amount"]):F2}</p>
        <hr />
        <p><strong>Payment Amount:</strong> Rs. {Convert.ToDouble(dr["amount"]):F2}</p>
        <p><strong>Payment Date:</strong> {Convert.ToDateTime(dr["payment_date"]):yyyy-MM-dd HH:mm}</p>
        <p><strong>Payment Method:</strong> {dr["method"]}</p>
        <p><strong>Status:</strong> {dr["status"]}</p>
        <p><strong>Transaction Reference:</strong> {dr["txn_ref"]}</p>
        <hr />
        <p class='text-success'>Thank you for your payment.</p>
    </div>
</div>";
                        btnDownloadPdf.Visible = true;
                    }
                    else
                    {
                        litReceipt.Text = "<div class='alert alert-warning'>Payment details not found.</div>";
                        btnDownloadPdf.Visible = false;
                    }
                }
            }
        }

        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            GeneratePdf(paymentId);
        }

        private void GeneratePdf(int paymentId)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT p.payment_id, p.consumer_number, p.amount, p.payment_date, p.method, p.status, p.txn_ref,
                           c.name as user_name, c.phone, c.email, c.address,
                           e.consumer_name, e.units_consumed, e.bill_amount, e.bill_date
                    FROM Payments p
                    LEFT JOIN Connections c ON c.consumer_number = p.consumer_number
                    LEFT JOIN ElectricityBill e ON e.consumer_number = p.consumer_number
                    WHERE p.payment_id = @pid", con);

                cmd.Parameters.AddWithValue("@pid", paymentId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return;

                    Document doc = new Document(PageSize.A4, 36, 36, 54, 54);
                    MemoryStream ms = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    writer.PageEvent = new PdfPageEvents();
                    doc.Open();

                    string logoPath = Server.MapPath("~/Styles/jbvnl_logo.png");
                    if (File.Exists(logoPath))
                    {
                        var logo = iTextSharp.text.Image.GetInstance(logoPath);
                        logo.ScaleToFit(100f, 50f);
                        logo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(logo);
                    }

                    var title = new Paragraph("JBVNL Electricity Bill Payment Receipt",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(41, 128, 185)))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20f
                    };
                    doc.Add(title);

                    PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 35f, 65f });

                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.WHITE);
                    Font valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);

                    void AddStyledRow(string label, string value)
                    {
                        PdfPCell labelCell = new PdfPCell(new Phrase(label, headerFont))
                        {
                            BackgroundColor = new BaseColor(52, 152, 219),
                            Border = Rectangle.NO_BORDER,
                            Padding = 6
                        };
                        PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont))
                        {
                            BackgroundColor = new BaseColor(245, 245, 245),
                            Border = Rectangle.NO_BORDER,
                            Padding = 6
                        };
                        table.AddCell(labelCell);
                        table.AddCell(valueCell);
                    }

                    AddStyledRow("Consumer Number:", dr["consumer_number"].ToString());
                    AddStyledRow("Consumer Name:", dr["consumer_name"].ToString());
                    AddStyledRow("Customer Name:", dr["user_name"].ToString());
                    AddStyledRow("Phone:", dr["phone"].ToString());
                    AddStyledRow("Email:", dr["email"].ToString());
                    AddStyledRow("Address:", dr["address"].ToString());
                    AddStyledRow("Bill Date:", Convert.ToDateTime(dr["bill_date"]).ToString("yyyy-MM-dd"));
                    AddStyledRow("Units Consumed (kWh):", dr["units_consumed"].ToString());
                    AddStyledRow("Bill Amount (Rs.):", Convert.ToDouble(dr["bill_amount"]).ToString("F2"));
                    AddStyledRow("Payment Amount (Rs.):", Convert.ToDouble(dr["amount"]).ToString("F2"));
                    AddStyledRow("Payment Date:", Convert.ToDateTime(dr["payment_date"]).ToString("yyyy-MM-dd HH:mm"));
                    AddStyledRow("Payment Method:", dr["method"].ToString());
                    AddStyledRow("Payment Status:", dr["status"].ToString());
                    AddStyledRow("Transaction Ref:", dr["txn_ref"].ToString());

                    doc.Add(table);

                    Paragraph footer = new Paragraph("\nThank you for your payment!\nJBVNL Utility Services",
                        FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12, BaseColor.GRAY))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 20f
                    };
                    doc.Add(footer);

                    doc.Close();

                    byte[] bytes = ms.ToArray();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", $"attachment;filename=Receipt_{paymentId}.pdf");
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    public class PdfPageEvents : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footerTbl = new PdfPTable(1)
            {
                TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
            };
            footerTbl.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Phrase("© 2025 JBVNL Electricity Board - All rights reserved.", FontFactory.GetFont(FontFactory.HELVETICA, 9)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };
            footerTbl.AddCell(cell);
            footerTbl.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin - 10, writer.DirectContent);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfPTable headerTbl = new PdfPTable(1)
            {
                TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
            };
            headerTbl.DefaultCell.Border = 0;

            PdfPCell cell = new PdfPCell(new Phrase("JBVNL Electricity Board", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingBottom = 10
            };
            headerTbl.AddCell(cell);
            headerTbl.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 20, writer.DirectContent);
        }
    }
}
