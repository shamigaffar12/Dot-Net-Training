using System; using System.IO; using System.Security.Cryptography; using System.Text; using System.Configuration; using System.Data.SqlClient;
public partial class Webhook : System.Web.UI.Page
{
    protected void Page_Load(object s, EventArgs e)
    {
        string body; using(var sr=new StreamReader(Request.InputStream)) body = sr.ReadToEnd();
        string sig = Request.Headers["X-Razorpay-Signature"] ?? Request.Form["razorpay_signature"] ?? "";
        string secret = ConfigurationManager.AppSettings["RazorpaySecret"];
        if(string.IsNullOrEmpty(secret)){ Response.StatusCode=400; Response.Write("No secret"); return; }
        var comp = ComputeHmac(body, secret);
        if(!string.Equals(comp, sig, StringComparison.OrdinalIgnoreCase)){ Response.StatusCode=400; Response.Write("Invalid"); return; }
        // For demo - insert a Payments row
        try{ string conn = ConfigurationManager.ConnectionStrings["EbDb"].ConnectionString; using(var c=new SqlConnection(conn)){ c.Open(); var cmd=new SqlCommand("INSERT INTO Payments(consumer_number,amount,method,txn_ref,status) VALUES('WEBHOOK',0,'razorpay','webhook_'+CONVERT(varchar(20),GETDATE(),120),'Success')",c); cmd.ExecuteNonQuery(); } } catch{}
        Response.StatusCode=200; Response.Write("ok"); 
    }
    string ComputeHmac(string data, string key){ var keyb=Encoding.UTF8.GetBytes(key); using(var h=new HMACSHA256(keyb)){ var hash=h.ComputeHash(Encoding.UTF8.GetBytes(data)); return BitConverter.ToString(hash).Replace("-","").ToLower(); } }
}