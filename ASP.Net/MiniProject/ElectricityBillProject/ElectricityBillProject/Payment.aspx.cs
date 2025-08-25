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
            // Optional: reset status on page load
        }

        protected void btnGenQR_Click(object sender, EventArgs e)
        {
            qrImage.Visible = true;
            lblStatus.Text = "Scan the QR code to initiate payment.";
            btnSubmit.Enabled = false;
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            // Simulate payment success/failure
            bool paymentSuccess = new Random().Next(0, 2) == 1;

            if (paymentSuccess)
            {
                lblStatus.Text = "✅ Payment successful. You may now submit.";
                btnSubmit.Enabled = true;
            }
            else
            {
                lblStatus.Text = "❌ Payment failed. Please try again.";
                btnSubmit.Enabled = false;
                qrImage.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = DBHandler.GetConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Payments(user_id,consumer_number,amount,method,status,txn_ref) VALUES(@u,@c,@a,@m,'Success',@r);", con);
                int uid = Session["user_id"] != null ? Convert.ToInt32(Session["user_id"]) : 0;
                cmd.Parameters.AddWithValue("@u", uid);
                cmd.Parameters.AddWithValue("@c", txtConsumer.Text);
                cmd.Parameters.AddWithValue("@a", Convert.ToDouble(txtAmount.Text));
                cmd.Parameters.AddWithValue("@m", ddlMethod.SelectedValue);
                cmd.Parameters.AddWithValue("@r", Guid.NewGuid().ToString().Substring(0, 10));
                con.Open(); cmd.ExecuteNonQuery(); con.Close();

                lblStatus.Text = "🎉 Payment recorded successfully!";
                btnSubmit.Enabled = false;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
            }
        }
    }
}