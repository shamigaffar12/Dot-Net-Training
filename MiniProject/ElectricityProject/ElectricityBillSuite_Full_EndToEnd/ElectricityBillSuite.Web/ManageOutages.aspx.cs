using System; using System.Data.SqlClient; using System.Configuration; using System.Text;
public partial class ManageOutages : System.Web.UI.Page
{
    protected void Page_Load(object s, EventArgs e) { if(!IsPostBack) Bind(); }
    protected void btnAdd_Click(object src, EventArgs e){
        string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("INSERT INTO Outages(Location,Status,StartTime,EndTime) VALUES(@l,@s,@st,@en)",c);
            cmd.Parameters.AddWithValue("@l", txtLoc.Text); cmd.Parameters.AddWithValue("@s", txtStatus.Text); cmd.Parameters.AddWithValue("@st", txtStart.Text); cmd.Parameters.AddWithValue("@en", txtEnd.Text); cmd.ExecuteNonQuery(); }
        Bind();
    }
    void Bind(){ var sb=new StringBuilder(); string conn=ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString; using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("SELECT Location,Status,StartTime,EndTime FROM Outages ORDER BY StartTime DESC",c); var r=cmd.ExecuteReader(); sb.Append("<ul>"); while(r.Read()){ sb.AppendFormat("<li><b>{0}</b> - {1} ({2} to {3})</li>", r[0], r[1], r[2], r[3]); } sb.Append("</ul>"); } litOut.Text=sb.ToString(); }
}