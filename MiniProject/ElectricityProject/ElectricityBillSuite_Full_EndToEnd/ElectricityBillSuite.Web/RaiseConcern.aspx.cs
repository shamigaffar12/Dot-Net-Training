using System; using System.Data.SqlClient; using System.Configuration;
public partial class RaiseConcern : System.Web.UI.Page
{
    protected void btnSend_Click(object s, EventArgs e)
    {
        if (Session["user_id"]==null) { Response.Redirect("UserLogin.aspx"); return; }
        int uid = Convert.ToInt32(Session["user_id"]);
        string subj=txtSubject.Text.Trim(), msg=txtMsg.Text.Trim();
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("INSERT INTO Concerns(user_id,subject,message,status,created_at) VALUES(@u,@s,@m,'Open',GETDATE())",c);
            cmd.Parameters.AddWithValue("@u",uid); cmd.Parameters.AddWithValue("@s",subj); cmd.Parameters.AddWithValue("@m",msg); cmd.ExecuteNonQuery(); }
        Response.Write("<div class='text-success'>Concern submitted.</div>");
    }
}