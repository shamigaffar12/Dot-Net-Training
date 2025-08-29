using ElectricityBillProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ElectricityBillProject
{
    public partial class ConsumerLookup : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string consumerNumber = txtConsumerNumber.Text.Trim();
            
            if (!string.IsNullOrEmpty(consumerNumber))
            {
                
                Response.Redirect($"BillReceipt.aspx?consumerNumber={HttpUtility.UrlEncode(consumerNumber)}");
            }
            else
            {
               
            }
        }
    }
}
