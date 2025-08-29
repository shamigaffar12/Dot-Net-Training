using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using DatabaseConnection;
using ElectricityBillProject.Models;
using System.Configuration;

namespace ElectricityBillProject
{
    public partial class UserDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblBillMsg.Text = "";
                litMessage.Text = "";
                pnlTransactions.Visible = false;
            }
        }

        protected void btnViewConnections_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            if (!ConsumerNumberValidator.IsValid(consumerNumber) || string.IsNullOrEmpty(consumerNumber))
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Please enter a valid consumer number.";
                pnlTransactions.Visible = false;
                return;
            }

            LoadTransactionHistory(consumerNumber);
        }

        private void LoadTransactionHistory(string consumerNumber)
        {
            var combinedTransactions = new List<TransactionItem>();

            using (var con = DBHandler.GetConnection())
            {
                con.Open();


                using (var cmd = new SqlCommand(
                    "SELECT bill_id, bill_date, units_consumed, bill_amount, due_date, status " +
                    "FROM Payments WHERE consumer_number = @cnum", con))
                {
                    cmd.Parameters.AddWithValue("@cnum", consumerNumber);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            combinedTransactions.Add(new TransactionItem
                            {
                                TransactionId = rdr["bill_id"].ToString(),
                                TransactionDate = (DateTime)rdr["bill_date"],
                                TransactionType = "Bill",
                                Details = $"Units: {rdr["units_consumed"]}, Due: {((DateTime)rdr["due_date"]).ToShortDateString()}, Status: {rdr["status"]}",
                                Amount = Convert.ToDecimal(rdr["bill_amount"])
                            });
                        }
                    }
                }

               
                using (var cmd = new SqlCommand(
                    "SELECT payment_id, payment_date, amount AS amount_paid, method AS payment_mode " +
                    "FROM Payments WHERE consumer_number = @cnum", con))
                {
                    cmd.Parameters.AddWithValue("@cnum", consumerNumber);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            combinedTransactions.Add(new TransactionItem
                            {
                                TransactionId = rdr["payment_id"].ToString(),
                                TransactionDate = (DateTime)rdr["payment_date"],
                                TransactionType = "Payment",
                                Details = $"Mode: {rdr["payment_mode"]}",
                                Amount = Convert.ToDecimal(rdr["amount_paid"])
                            });
                        }
                    }
                }

               
                using (var cmd = new SqlCommand(
                    "SELECT concern_id, created_at, message AS subject, status FROM Concerns WHERE consumer_number = @cnum", con))
                {
                    cmd.Parameters.AddWithValue("@cnum", consumerNumber);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            combinedTransactions.Add(new TransactionItem
                            {
                                TransactionId = rdr["concern_id"].ToString(),
                                TransactionDate = (DateTime)rdr["created_at"],
                                TransactionType = "Concern",
                                Details = $"Subject: {rdr["subject"]}, Status: {rdr["status"]}",
                                Amount = 0m
                            });
                        }
                    }
                }
            }

            combinedTransactions.Sort((x, y) => y.TransactionDate.CompareTo(x.TransactionDate));
            gvTransactions.DataSource = combinedTransactions;
            gvTransactions.DataBind();

            pnlTransactions.Visible = true;
            lblBillMsg.Text = "";
        }

        protected void gvTransactions_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownloadReceipt")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                if (args.Length == 2 && args[1] == "Payment")
                {
                    GeneratePaymentReceiptAndSend(args[0], txtConsumerNumber.Text.Trim());
                }
                else
                {
                    lblBillMsg.CssClass = "text-warning";
                    lblBillMsg.Text = "Receipt download is available only for payments.";
                }
            }
        }

        private void GeneratePaymentReceiptAndSend(string paymentId, string consumerNumber)
        {
            try
            {
                byte[] pdfBytes = GeneratePaymentReceiptPdfBytes(paymentId, consumerNumber);
                if (pdfBytes == null)
                {
                    lblBillMsg.CssClass = "text-danger";
                    lblBillMsg.Text = "Payment details not found.";
                    return;
                }

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename=PaymentReceipt_{paymentId}.pdf");
                Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Error generating payment receipt: " + ex.Message;
            }
        }

        private byte[] GeneratePaymentReceiptPdfBytes(string paymentId, string consumerNumber)
        {
            using (var con = DBHandler.GetConnection())
            {
                con.Open();
                var cmd = new SqlCommand(
                    "SELECT p.payment_id, p.payment_date, p.amount AS amount_paid, p.method AS payment_mode, c.name AS consumer_name " +
                    "FROM Payments p " +
                    "JOIN Connections c ON p.consumer_number = c.consumer_number " +
                    "WHERE p.payment_id = @pid AND p.consumer_number = @cnum", con);
                cmd.Parameters.AddWithValue("@pid", paymentId);
                cmd.Parameters.AddWithValue("@cnum", consumerNumber);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    using (var ms = new MemoryStream())
                    {
                        var doc = new Document(PageSize.A4, 36, 36, 54, 54);
                        var writer = PdfWriter.GetInstance(doc, ms);
                        doc.Open();

                        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLUE);
                        var subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.GRAY);
                        var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                        var footerFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10, BaseColor.DARK_GRAY);

                        doc.Add(new Paragraph("⚡ Payment Receipt ⚡", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 20f
                        });

                        var infoTable = new PdfPTable(2) { WidthPercentage = 90 };
                        infoTable.SetWidths(new float[] { 1f, 2f });

                        void AddCell(string text, Font font, bool isHeader = false)
                        {
                            var cell = new PdfPCell(new Phrase(text, font)) { Border = Rectangle.NO_BORDER, Padding = 5 };
                            if (isHeader)
                                cell.BackgroundColor = new BaseColor(230, 230, 250);
                            infoTable.AddCell(cell);
                        }

                        AddCell("Consumer Name:", subTitleFont, true);
                        AddCell(rdr["consumer_name"].ToString(), bodyFont);
                        AddCell("Consumer Number:", subTitleFont, true);
                        AddCell(consumerNumber, bodyFont);
                        AddCell("Payment ID:", subTitleFont, true);
                        AddCell(rdr["payment_id"].ToString(), bodyFont);
                        AddCell("Payment Date:", subTitleFont, true);
                        AddCell(((DateTime)rdr["payment_date"]).ToString("dd MMM yyyy HH:mm"), bodyFont);
                        AddCell("Amount Paid (₹):", subTitleFont, true);
                        AddCell(Convert.ToDecimal(rdr["amount_paid"]).ToString("N2"), bodyFont);
                        AddCell("Payment Mode:", subTitleFont, true);
                        AddCell(rdr["payment_mode"].ToString(), bodyFont);

                        doc.Add(infoTable);
                        var thanks = new Paragraph("\nThank you for your payment!", footerFont)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        doc.Add(thanks);

                        doc.Close();
                        return ms.ToArray();
                    }
                }
            }
        }

        protected void btnDownloadBill_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            if (!ConsumerNumberValidator.IsValid(consumerNumber) || string.IsNullOrEmpty(consumerNumber))
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Please enter a valid consumer number.";
                return;
            }

            byte[] billPdf = GenerateBillPdf(consumerNumber);
            if (billPdf == null)
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "No bill found for this consumer number.";
                return;
            }

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", $"attachment;filename=ElectricityBill_{consumerNumber}.pdf");
            Response.OutputStream.Write(billPdf, 0, billPdf.Length);
            Response.Flush();
            Response.End();
        }

        private byte[] GenerateBillPdf(string consumerNumber)
        {
            using (var con = DBHandler.GetConnection())
            {
                con.Open();
                var cmd = new SqlCommand(
                    "SELECT c.name AS consumer_name, c.address AS consumer_address, " +
                    "e.bill_id, e.bill_date, e.units_consumed, e.bill_amount, e.due_date, e.status " +
                    "FROM Connections c " +
                    "INNER JOIN ElectricityBill e ON c.consumer_number = e.consumer_number " +
                    "WHERE e.consumer_number = @cnum ORDER BY e.bill_date DESC", con);
                cmd.Parameters.AddWithValue("@cnum", consumerNumber);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    using (var ms = new MemoryStream())
                    {
                        var doc = new Document(PageSize.A4, 36, 36, 54, 54);
                        var writer = PdfWriter.GetInstance(doc, ms);
                        doc.Open();

                        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, new BaseColor(0, 102, 204));
                        var subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
                        var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

                        doc.Add(new Paragraph("⚡ Electricity Bill ⚡", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 25f
                        });

                        var consTable = new PdfPTable(2) { WidthPercentage = 90 };
                        consTable.SetWidths(new float[] { 1f, 2f });

                        void AddConsCell(string text, Font font, bool isHeader = false)
                        {
                            var cell = new PdfPCell(new Phrase(text, font)) { Border = Rectangle.NO_BORDER, Padding = 6 };
                            if (isHeader)
                                cell.BackgroundColor = new BaseColor(224, 224, 224);
                            consTable.AddCell(cell);
                        }

                        AddConsCell("Consumer Name:", subTitleFont, true);
                        AddConsCell(rdr["consumer_name"].ToString(), bodyFont);
                        AddConsCell("Address:", subTitleFont, true);
                        AddConsCell(rdr["consumer_address"].ToString(), bodyFont);
                        AddConsCell("Consumer Number:", subTitleFont, true);
                        AddConsCell(consumerNumber, bodyFont);

                        doc.Add(consTable);
                        doc.Add(new Paragraph("\n"));

                        var billTable = new PdfPTable(2) { WidthPercentage = 90 };
                        billTable.SetWidths(new float[] { 1f, 2f });

                        void AddBillCell(string text, Font font, bool isHeader = false)
                        {
                            var cell = new PdfPCell(new Phrase(text, font)) { Border = Rectangle.NO_BORDER, Padding = 6 };
                            if (isHeader)
                                cell.BackgroundColor = new BaseColor(180, 180, 180);
                            billTable.AddCell(cell);
                        }

                        AddBillCell("Bill ID:", subTitleFont, true);
                        AddBillCell(rdr["bill_id"].ToString(), bodyFont);
                        AddBillCell("Bill Date:", subTitleFont, true);
                        AddBillCell(((DateTime)rdr["bill_date"]).ToString("dd MMM yyyy"), bodyFont);
                        AddBillCell("Units Consumed:", subTitleFont, true);
                        AddBillCell(rdr["units_consumed"].ToString(), bodyFont);
                        AddBillCell("Amount (₹):", subTitleFont, true);
                        AddBillCell(Convert.ToDecimal(rdr["bill_amount"]).ToString("N2"), bodyFont);
                        AddBillCell("Due Date:", subTitleFont, true);
                        AddBillCell(((DateTime)rdr["due_date"]).ToString("dd MMM yyyy"), bodyFont);
                        AddBillCell("Status:", subTitleFont, true);
                        BaseColor statusColor = rdr["status"].ToString().ToLower() == "paid" ? BaseColor.GREEN : BaseColor.RED;
                        billTable.AddCell(new PdfPCell(new Phrase(rdr["status"].ToString(), new Font(bodyFont) { Color = statusColor }))
                        { Border = Rectangle.NO_BORDER, Padding = 6 });

                        doc.Add(billTable);
                        doc.Add(new Paragraph("\nThank you for using our services!", bodyFont));

                        doc.Close();
                        return ms.ToArray();
                    }
                }
            }
        }

        protected void btnPrintBill_Click(object sender, EventArgs e)
        {
            btnDownloadBill_Click(sender, e);
        }

        protected void btnEmailBill_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();

            if (string.IsNullOrEmpty(consumerNumber))
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Please enter a consumer number.";
                return;
            }

           
            string email = GetEmailByConsumerNumber(consumerNumber);
            if (string.IsNullOrEmpty(email))
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "No email found for this consumer number.";
                return;
            }

            byte[] billPdf = GenerateBillPdf(consumerNumber);
            if (billPdf == null)
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "No bill found to email.";
                return;
            }

            try
            {
                using (var mail = new System.Net.Mail.MailMessage("no-reply@ebillsuite.local", email))
                {
                    mail.Subject = $"Electricity Bill for Consumer {consumerNumber}";
                    mail.Body = "Please find attached your electricity bill.";
                    mail.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(billPdf), $"ElectricityBill_{consumerNumber}.pdf"));

                    using (var smtp = new System.Net.Mail.SmtpClient())
                    {
                        smtp.Send(mail);
                    }
                }

                lblBillMsg.CssClass = "text-success";
                lblBillMsg.Text = "Bill emailed successfully!";
            }
            catch (Exception ex)
            {
                lblBillMsg.CssClass = "text-danger";
                lblBillMsg.Text = "Failed to send email: " + ex.Message;
            }
        }

       
        private string GetEmailByConsumerNumber(string consumerNumber)
        {
            string email = null;
            string connString = ConfigurationManager.ConnectionStrings["ElectricityBillDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT email FROM Connections WHERE consumer_number = @consumerNumber";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@consumerNumber", consumerNumber);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        email = result.ToString();
                    }
                }
            }
            return email;
        }



        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }

    public class TransactionItem
    {
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Details { get; set; }
        public decimal Amount { get; set; }
    }
}
