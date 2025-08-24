using System; using System.Data.SqlClient; using System.Configuration;
public partial class AdminLogin : System.Web.UI.Page
{
    protected void btnLogin_Click(object s, EventArgs e)
    {
        string u=txtUser.Text.Trim(), p=txtPass.Text;
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("SELECT admin_id FROM Admins WHERE username=@u AND password=@p",c); cmd.Parameters.AddWithValue("@u",u); cmd.Parameters.AddWithValue("@p",p); var r=cmd.ExecuteScalar();
            if(r!=null) { Session["admin_id"]=r; Response.Redirect("AdminDashboard.aspx"); }
            else Response.Write("<div class='text-danger'>Invalid admin</div>"); }
    }
}