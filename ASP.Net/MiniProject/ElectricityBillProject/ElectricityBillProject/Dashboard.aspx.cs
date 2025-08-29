using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseConnection;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ElectricityBillProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            userId = Session["user_id"] != null ? Convert.ToInt32(Session["user_id"]) : 0;

            if (userId == 0)
            {
                Response.Redirect("UserLogin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadDashboardSummary();
                LoadProfile();
                LoadConnections();
                LoadBills();
                LoadTransactions();
                LoadConcerns();
            }
        }

    

        private void LoadDashboardSummary()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();
                string query = @"
                    SELECT ISNULL(SUM(bill_amount), 0) AS TotalDue
                    FROM ElectricityBill
                    WHERE consumer_number IN (SELECT consumer_number FROM Connections WHERE user_id = @u)
                    AND status = 'Unpaid';

                    SELECT TOP 1 payment_date FROM Payments WHERE user_id = @u ORDER BY payment_date DESC;
                ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", userId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblDue.Text = "₹" + dr["TotalDue"].ToString();
                    }
                    if (dr.NextResult() && dr.Read())
                    {
                        lblLastPay.Text = Convert.ToDateTime(dr["payment_date"]).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        lblLastPay.Text = "--";
                    }
                }
            }
        }

        private void LoadProfile()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = "SELECT name, phone, email, dob FROM Users WHERE user_id = @u";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", userId);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtPhone.Text = dr["phone"].ToString();
                        txtEmail.Text = dr["email"].ToString();
                        txtDOB.Text = dr["dob"] != DBNull.Value ? Convert.ToDateTime(dr["dob"]).ToString("yyyy-MM-dd") : "";
                    }
                }
            }
        }

        private void LoadConnections()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = "SELECT consumer_number, connection_address, connection_type, connection_status FROM Connections WHERE user_id = @u";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@u", userId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvConnections.DataSource = dt;
                gvConnections.DataBind();
            }
        }

        private void LoadBills()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = @"
                    SELECT bill_id, consumer_number, consumer_name, units_consumed, bill_amount, bill_date, due_date, status
                    FROM ElectricityBill
                    WHERE consumer_number IN (SELECT consumer_number FROM Connections WHERE user_id = @u)
                    ORDER BY bill_date DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@u", userId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvBills.DataSource = dt;
                gvBills.DataBind();
            }
        }

        private void LoadTransactions()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = @"
                    SELECT payment_id, consumer_number, amount, payment_date, method, status, txn_ref
                    FROM Payments
                    WHERE user_id = @u
                    ORDER BY payment_date DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@u", userId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvTransactions.DataSource = dt;
                gvTransactions.DataBind();
            }
        }

        private void LoadConcerns()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = @"
                    SELECT concern_id, consumer_number, message, status, created_at, resolved_at
                    FROM Concerns
                    WHERE user_id = @u
                    ORDER BY created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@u", userId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvConcerns.DataSource = dt;
                gvConcerns.DataBind();
            }
        }

      

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    string query = "UPDATE Users SET name=@name, phone=@phone, email=@email, dob=@dob WHERE user_id=@u";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    if (DateTime.TryParse(txtDOB.Text, out DateTime dob))
                        cmd.Parameters.AddWithValue("@dob", dob);
                    else
                        cmd.Parameters.AddWithValue("@dob", DBNull.Value);

                    cmd.Parameters.AddWithValue("@u", userId);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblProfileMsg.ForeColor = System.Drawing.Color.Green;
                    lblProfileMsg.Text = "Profile updated successfully.";
                }
            }
            catch (Exception ex)
            {
                lblProfileMsg.ForeColor = System.Drawing.Color.Red;
                lblProfileMsg.Text = "Error updating profile.";
            }
        }

        

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("UserLogin.aspx");
        }

      

        protected void gvBills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!int.TryParse(e.CommandArgument.ToString(), out int billId))
            {
                lblMsg.Text = "Invalid bill selected.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            DataRow bill = GetBillById(billId);

            if (bill == null)
            {
                lblMsg.Text = "Bill not found.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                if (e.CommandName == "DownloadPdf" || e.CommandName == "Print")
                {
                    GeneratePdfReceipt(bill);

                  
                }
                else if (e.CommandName == "EmailReceipt")
                {
                    SendBillReceiptEmail(bill);
                    lblMsg.Text = "Bill receipt emailed successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error processing request: " + ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        private DataRow GetBillById(int billId)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = @"
                    SELECT bill_id, consumer_number, consumer_name, units_consumed, bill_amount, bill_date, due_date, status
                    FROM ElectricityBill
                    WHERE bill_id = @billId";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@billId", billId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                    return null;

                return dt.Rows[0];
            }
        }

        private void GeneratePdfReceipt(DataRow bill)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, ms);
                document.Open();

                BaseColor headerColor = new BaseColor(0, 102, 204); // Blue
                BaseColor labelColor = BaseColor.BLACK;
                BaseColor valueColor = new BaseColor(80, 80, 80);

                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, headerColor);
                Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, labelColor);
                Font valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, valueColor);

              
                Paragraph title = new Paragraph("Electricity Bill Receipt", headerFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                document.Add(title);

                PdfPTable table = new PdfPTable(2) { WidthPercentage = 80 };
                table.SetWidths(new float[] { 2f, 3f });

                void AddCell(string text, Font font, BaseColor bgColor, int colspan = 1)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(text, font))
                    {
                        BackgroundColor = bgColor,
                        Colspan = colspan,
                        Padding = 5,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.NO_BORDER
                    };
                    table.AddCell(cell);
                }

                AddCell("Consumer Number:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell(bill["consumer_number"].ToString(), valueFont, BaseColor.WHITE);

                AddCell("Consumer Name:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell(bill["consumer_name"].ToString(), valueFont, BaseColor.WHITE);

                AddCell("Units Consumed:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell(bill["units_consumed"].ToString(), valueFont, BaseColor.WHITE);

                AddCell("Bill Amount:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell("₹" + Convert.ToDecimal(bill["bill_amount"]).ToString("N2"), valueFont, BaseColor.WHITE);

                AddCell("Bill Date:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell(Convert.ToDateTime(bill["bill_date"]).ToString("yyyy-MM-dd"), valueFont, BaseColor.WHITE);

                AddCell("Due Date:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell(Convert.ToDateTime(bill["due_date"]).ToString("yyyy-MM-dd"), valueFont, BaseColor.WHITE);

                AddCell("Status:", labelFont, BaseColor.LIGHT_GRAY);
                AddCell(bill["status"].ToString(), valueFont, BaseColor.WHITE);

                document.Add(table);

                document.Close();

                byte[] bytes = ms.ToArray();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"attachment; filename=Bill_{bill["consumer_number"]}_{bill["bill_id"]}.pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();
              
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        private void SendBillReceiptEmail(DataRow bill)
        {
          

            string emailTo = GetUserEmail(userId);
            if (string.IsNullOrEmpty(emailTo))
                throw new Exception("User email not found.");

            MailMessage mail = new MailMessage();
            mail.To.Add(emailTo);
            mail.Subject = $"Electricity Bill Receipt - Bill ID {bill["bill_id"]}";
            mail.Body = $@"
Dear Customer,

Please find your electricity bill details below:

Consumer Number: {bill["consumer_number"]}
Consumer Name: {bill["consumer_name"]}
Units Consumed: {bill["units_consumed"]}
Bill Amount: ₹{Convert.ToDecimal(bill["bill_amount"]).ToString("N2")}
Bill Date: {Convert.ToDateTime(bill["bill_date"]).ToString("yyyy-MM-dd")}
Due Date: {Convert.ToDateTime(bill["due_date"]).ToString("yyyy-MM-dd")}
Status: {bill["status"]}

Thank you for your prompt payment.

Best Regards,
Electricity Billing Team
";

          
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph p = new Paragraph(mail.Body);
                doc.Add(p);

                doc.Close();

                ms.Position = 0;
                mail.Attachments.Add(new Attachment(ms, $"Bill_{bill["consumer_number"]}_{bill["bill_id"]}.pdf"));
                SendEmail(mail);
            }
        }

        private void SendEmail(MailMessage mail)
        {
            
            using (SmtpClient smtp = new SmtpClient("smtp.yourserver.com"))
            {
                smtp.Port = 587; 
                smtp.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
        }

        private string GetUserEmail(int uid)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = "SELECT email FROM Users WHERE user_id = @u";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", uid);
                con.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }

       
    }
}
