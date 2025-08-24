using System; using System.Data.SqlClient; using System.Configuration;
public partial class Register : System.Web.UI.Page
{
    protected void btnReg_Click(object s, EventArgs e)
    {
        string name=txtName.Text.Trim(), phone=txtPhone.Text.Trim(), email=txtEmail.Text.Trim(), dob=txtDob.Text.Trim(), pass=txtPass.Text;
        if(name==""||email=="") { litMsg.Text="<div class='text-danger'>Name & Email required</div>"; return; }
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("INSERT INTO Users(name,phone,email,dob,password) VALUES(@n,@p,@e,@d,@pw)",c);
            cmd.Parameters.AddWithValue("@n",name); cmd.Parameters.AddWithValue("@p",phone); cmd.Parameters.AddWithValue("@e",email); cmd.Parameters.AddWithValue("@d",dob); cmd.Parameters.AddWithValue("@pw",pass); cmd.ExecuteNonQuery(); }
        litMsg.Text="<div class='text-success'>Registered. Please login.</div>";
    }
}