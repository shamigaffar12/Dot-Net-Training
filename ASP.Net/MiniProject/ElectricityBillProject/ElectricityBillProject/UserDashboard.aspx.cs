using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ElectricityBillProject
{
    public partial class UserDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Check if user is logged in
            if (Session["user_id"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("UserLogin.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx"); // or some previous page
        }
    }
}
