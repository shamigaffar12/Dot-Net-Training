using System; using System.Text; using System.Data.SqlClient; using System.Configuration;
public partial class UserDashboard : System.Web.UI.Page
{
    protected void Page_Load(object s, EventArgs e)
    {
        if (Session["user_id"]==null) { Response.Redirect("UserLogin.aspx"); return; }
        int uid = Convert.ToInt32(Session["user_id"]);
        StringBuilder sb = new StringBuilder();
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("SELECT consumer_number,name,status FROM Connections WHERE user_id=@u",c); cmd.Parameters.AddWithValue("@u",uid); var r=cmd.ExecuteReader();
            sb.Append("<table class='table-modern'><thead><tr><th>Consumer</th><th>Name</th><th>Status</th><th>Action</th></tr></thead><tbody>"); while(r.Read()){
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td><a class='btn btn-sm btn-outline' href='ViewBills.aspx?c={0}'>View Bills</a></td></tr>", r[0], r[1], r[2]);
            } sb.Append("</tbody></table>"); }
        litContent.Text = sb.ToString();
    }
}