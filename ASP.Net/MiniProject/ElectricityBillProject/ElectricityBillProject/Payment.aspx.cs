using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseConnection;

namespace ElectricityBillProject
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                qrSection.Visible = true;
                receiptSection.Visible = false;
            }
        }

        protected void btnGenerateQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConsumer.Text) || string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                lblMsg.Text = "⚠️ Please enter both consumer number and amount.";
                return;
            }

            qrSection.Visible = true;
            lblMsg.Text = "";
        }

        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            string consumer = txtConsumer.Text.Trim();
            string amountText = txtAmount.Text.Trim();

            if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                lblMsg.Text = "⚠️ Invalid amount.";
                return;
            }

            int userId = Convert.ToInt32(Session["user_id"] ?? "0");

            try
            {
                string txnRef = "TXN" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        INSERT INTO Payments(user_id, consumer_number, amount, payment_date, method, status, txn_ref)
                        VALUES(@uid, @consumer, @amount, GETDATE(), 'UPI-StaticQR', 'Success', @ref);

                        UPDATE ElectricityBill 
                        SET status='Paid' 
                        WHERE consumer_number=@consumer AND status='Unpaid';";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@consumer", consumer);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@ref", txnRef);
                        cmd.ExecuteNonQuery();
                    }
                }

                litReceipt.Text = $@"
                    <p><strong>Consumer Number:</strong> {consumer}</p>
                    <p><strong>Amount Paid:</strong> ₹{amount}</p>
                    <p><strong>Transaction Ref:</strong> {txnRef}</p>
                    <p><strong>Date:</strong> {DateTime.Now:yyyy-MM-dd HH:mm}</p>
                    <p><strong>Payment Mode:</strong> UPI Static QR</p>
                    <p class='text-success fw-bold'>✅ Payment Successful</p>";

                qrSection.Visible = false;
                receiptSection.Style["display"] = "block";
                lblMsg.Text = "";
            }
            catch (Exception ex)
            {
                lblMsg.Text = "❌ Error processing payment: " + ex.Message;
            }
        }

        protected void btnEmailPDF_Click(object sender, EventArgs e)
        {
            // Email + PDF logic goes here
            lblMsg.Text = "📧 PDF has been emailed (simulated).";
        }
    }
}
