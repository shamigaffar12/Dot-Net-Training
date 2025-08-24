using System; using System.IO;
public partial class NewConnection : System.Web.UI.Page
{
    protected void btnApply_Click(object s, EventArgs e)
    {
        string uploads = Server.MapPath("~/Uploads"); if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
        var idf = Request.Files["idProof"]; var photo = Request.Files["photo"];
        if (idf!=null && idf.ContentLength>0) { var p = Path.Combine(uploads, Path.GetFileName(idf.FileName)); idf.SaveAs(p); }
        if (photo!=null && photo.ContentLength>0) { var p = Path.Combine(uploads, Path.GetFileName(photo.FileName)); photo.SaveAs(p); }
        // create connection record (omitted) then redirect to payment for ₹8000
        Response.Redirect("Payment.aspx?amount=8000&purpose=new_connection");
    }
}