using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using DatabaseConnection;
using ElectricityBillProject.Models;

namespace ElectricityBillProject
{
    public partial class ViewBill : System.Web.UI.Page
    {
        protected void btnGo_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            


            int n = 5;
            if (!int.TryParse(txtN.Text.Trim(), out n) || n <= 0)
            {
                n = 5; 
            }

            if (string.IsNullOrEmpty(consumerNumber))
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Please enter a consumer number.";
                grid.DataSource = null;
                grid.DataBind();
                return;
            }

            string statusFilter = ddlStatus.SelectedValue;

            try
            {
                using (SqlConnection con = DBHandler.GetConnection())
                {
                    con.Open();

                    string query = @"
                        SELECT TOP (@n) consumer_number, consumer_name, units_consumed, bill_amount, bill_date, status
                        FROM ElectricityBill
                        WHERE consumer_number = @consumerNumber";

                    if (statusFilter != "All")
                    {
                        query += " AND status = @status";
                    }

                    query += " ORDER BY bill_date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@n", n);
                        cmd.Parameters.AddWithValue("@consumerNumber", consumerNumber);

                        if (statusFilter != "All")
                        {
                            cmd.Parameters.AddWithValue("@status", statusFilter);
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                lblMsg.CssClass = "text-info";
                                lblMsg.Text = "No bills found matching your criteria.";
                                grid.DataSource = null;
                            }
                            else
                            {
                                lblMsg.CssClass = "text-success";
                                lblMsg.Text = $"Showing latest {dt.Rows.Count} bill(s) for consumer {consumerNumber} (Status: {statusFilter}):";
                                grid.DataSource = dt;
                            }

                            grid.DataBind();
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error loading bills. Please try again.";
                grid.DataSource = null;
                grid.DataBind();
            }
        }
    }
}
