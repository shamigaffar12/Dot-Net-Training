using System;
using System.Data;
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
                qrImage.Visible = false;
                btnSubmit.Enabled = false;
                lblStatus.Text = string.Empty;

                LoadOutstandingBills();
            }
        }

      
        private void LoadOutstandingBills()
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                string query = @"
                    SELECT bill_id, consumer_number, consumer_name, units_consumed, bill_amount, bill_date, due_date
                    FROM ElectricityBill
                    WHERE consumer_number = @c AND status = 'Unpaid'
                    ORDER BY bill_date DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@c", txtConsumer.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvOutstanding.DataSource = dt;
                    gvOutstanding.DataBind();
                }
            }
        }

        protected void btnGenQR_Click(object sender, EventArgs e)
        {
            qrImage.ImageUrl = "~/Styles/myQR.png";
            qrImage.Visible = true;
            lblStatus.Text = "📱 Scan QR to pay.";
            btnSubmit.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "showQR", "showQR();", true);
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            bool success = new Random().Next(0, 2) == 1;
            lblStatus.Text = success
                ? "✅ Payment successful. Submit to confirm."
                : "❌ Payment failed. Try again.";
            lblStatus.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            btnSubmit.Enabled = success;
            qrImage.Visible = success;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumer.Text.Trim();
            if (!double.TryParse(txtAmount.Text, out double amount))
            {
                lblStatus.Text = "⚠ Please enter a valid amount.";
                lblStatus.ForeColor = System.Drawing.Color.OrangeRed;
                return;
            }

            int uid = Session["user_id"] != null
                ? Convert.ToInt32(Session["user_id"])
                : GetUserIdByConsumer(consumerNumber);

            if (uid <= 0)
            {
                lblStatus.Text = "❌ Invalid consumer number.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = DBHandler.GetConnection())
            {
                con.Open();

                string insert = @"INSERT INTO Payments(user_id, consumer_number, amount, method, status, txn_ref)
                                  VALUES(@u,@c,@a,@m,'Success',@r)";
                using (SqlCommand cmd = new SqlCommand(insert, con))
                {
                    cmd.Parameters.AddWithValue("@u", uid);
                    cmd.Parameters.AddWithValue("@c", consumerNumber);
                    cmd.Parameters.AddWithValue("@a", amount);
                    cmd.Parameters.AddWithValue("@m", ddlMethod.SelectedValue);
                    cmd.Parameters.AddWithValue("@r", Guid.NewGuid().ToString().Substring(0, 10));
                    cmd.ExecuteNonQuery();
                }

           
                string update = @"
                    UPDATE TOP(1) ElectricityBill
                    SET status = 'Paid'
                    WHERE consumer_number = @c AND status = 'Unpaid'
                    ORDER BY bill_date DESC";
                using (SqlCommand cmd = new SqlCommand(update, con))
                {
                    cmd.Parameters.AddWithValue("@c", consumerNumber);
                    cmd.ExecuteNonQuery();
                }

              


                con.Close();
            }

            lblStatus.Text = "🎉 Payment recorded and latest bill marked Paid!";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            btnSubmit.Enabled = false;

            
            LoadOutstandingBills();
        }

        private int GetUserIdByConsumer(string consumerNumber)
        {
            using (SqlConnection con = DBHandler.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT user_id FROM Connections WHERE consumer_number=@c", con))
                {
                    cmd.Parameters.AddWithValue("@c", consumerNumber);
                    con.Open();
                    object res = cmd.ExecuteScalar();
                    return res != null ? Convert.ToInt32(res) : 0;
                }
            }
        }
    }
}
