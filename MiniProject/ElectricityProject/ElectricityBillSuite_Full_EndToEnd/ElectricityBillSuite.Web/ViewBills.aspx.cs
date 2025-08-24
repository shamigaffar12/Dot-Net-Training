using System; using System.Text; using System.Configuration; using System.Data.SqlClient;
public partial class ViewBills : System.Web.UI.Page
{
    protected void Page_Load(object s, EventArgs e)
    {
        string c = Request.QueryString["c"]; if (string.IsNullOrEmpty(c)) { Response.Write("No consumer"); return; }
        StringBuilder sb = new StringBuilder(); string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString;
        using(var cn=new SqlConnection(conn)){ cn.Open(); var cmd=new SqlCommand("SELECT bill_id,units_consumed,bill_amount,bill_date,status FROM ElectricityBill WHERE consumer_number=@c ORDER BY bill_date DESC",cn); cmd.Parameters.AddWithValue("@c",c); var r=cmd.ExecuteReader();
            sb.Append("<table class='table-modern'><thead><tr><th>Date</th><th>Units</th><th>Amount</th><th>Status</th><th>Action</th></tr></thead><tbody>"); while(r.Read()){
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>₹{2}</td><td>{3}</td><td><a class='btn btn-sm btn-primary' href='Payment.aspx?bill={0}&amt={2}'>Pay</a> <a class='btn btn-sm btn-outline' href='DownloadBill.aspx?id={0}'>Download</a></td></tr>", r[4], r[1], r[2], r[3], r[0]);
            } sb.Append("</tbody></table>"); }
        litBills.Text = sb.ToString();
    }
}