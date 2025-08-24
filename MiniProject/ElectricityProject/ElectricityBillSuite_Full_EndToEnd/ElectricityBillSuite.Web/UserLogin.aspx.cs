using System; using System.Data.SqlClient; using System.Configuration; using System.Web.Security;
public partial class UserLogin : System.Web.UI.Page
{
    protected void btnLogin_Click(object s, EventArgs e)
    {
        string email=txtEmail.Text.Trim(), pass=txtPass.Text;
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("SELECT user_id FROM Users WHERE email=@e AND password=@p",c); cmd.Parameters.AddWithValue("@e",email); cmd.Parameters.AddWithValue("@p",pass); var r=cmd.ExecuteScalar();
            if(r!=null){ Session["user_id"]=r; Response.Redirect("UserDashboard.aspx"); } else litMsg.Text="<div class='text-danger'>Invalid</div>";
        }
    }
}