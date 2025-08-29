using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using ElectricityBillProject.Models;

namespace ElectricityBillProject
{
    public partial class AddBill : System.Web.UI.Page
    {
        ElectricityBoard eb = new ElectricityBoard();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                pnlAskCount.Visible = true;
                pnlInputBills.Visible = false;
                lblMsg.Text = "";
                lblMsg.CssClass = "text-danger";
            }
        }

        protected void btnSetCount_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!int.TryParse(txtBillCount.Text.Trim(), out int count) || count <= 0 || count > 20)
            {
                lblMsg.Text = "Please enter a valid number between 1 and 20.";
                return;
            }

            rptBills.DataSource = new int[count];
            rptBills.DataBind();

            pnlAskCount.Visible = false;
            pnlInputBills.Visible = true;
        }

        protected void btnGenerateBills_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            int successCount = 0;
            int errorCount = 0;

            var validator = new BillValidator();

            foreach (RepeaterItem item in rptBills.Items)
            {
                var txtConsumer = (TextBox)item.FindControl("txtConsumer");
                var txtName = (TextBox)item.FindControl("txtName");
                var txtUnits = (TextBox)item.FindControl("txtUnits");

                if (txtConsumer == null || txtName == null || txtUnits == null)
                    continue;

                string consumerNumber = txtConsumer.Text.Trim();
                string consumerName = txtName.Text.Trim();
                string unitsText = txtUnits.Text.Trim();

                if (string.IsNullOrEmpty(consumerNumber) && string.IsNullOrEmpty(consumerName) && string.IsNullOrEmpty(unitsText))
                    continue;

                try
                {
                    ValidateConsumerNumber(consumerNumber);

                    if (!int.TryParse(unitsText, out int unitsConsumed))
                    {
                        errorCount++;
                        continue;
                    }

                    string validationMsg = validator.ValidateUnitsConsumed(unitsConsumed);
                    if (validationMsg != "OK")
                    {
                        errorCount++;
                        continue;
                    }

                    var bill = new ElectricityBill
                    {
                        ConsumerNumber = consumerNumber,
                        ConsumerName = consumerName,
                        UnitsConsumed = unitsConsumed
                    };

                    eb.CalculateBill(bill);
                    eb.AddBill(bill);

                    successCount++;
                }
                catch
                {
                    errorCount++;
                }
            }

            if (successCount > 0)
            {
                lblMsg.CssClass = "text-success";
                lblMsg.Text = $"Successfully added {successCount} bill(s).";
            }
            else
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "No valid bills to add.";
            }

            if (errorCount > 0)
                lblMsg.Text += $" {errorCount} bill(s) had errors and were skipped.";

         
            pnlInputBills.Visible = false;
            pnlAskCount.Visible = true;
            txtBillCount.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlInputBills.Visible = false;
            pnlAskCount.Visible = true;
            lblMsg.Text = "";
            txtBillCount.Text = "";
        }

        private void ValidateConsumerNumber(string consumerNumber)
        {
            if (string.IsNullOrEmpty(consumerNumber) || !Regex.IsMatch(consumerNumber, @"^EB\d{5}$"))
                throw new FormatException("Invalid Consumer Number");
        }
    }
}
