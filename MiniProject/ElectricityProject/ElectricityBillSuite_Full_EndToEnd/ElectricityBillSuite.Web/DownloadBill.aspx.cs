using System; using System.IO; using System.Collections.Generic;
public partial class DownloadBill : System.Web.UI.Page
{
    protected void Page_Load(object s, EventArgs e)
    {
        string id = Request.QueryString["id"]; if (string.IsNullOrEmpty(id)) { Response.Write("Invalid"); return; }
        // fetch bill details from DB (omitted for brevity) and create PDF
        var fields = new Dictionary<string,string>(); fields["Bill ID"] = id; fields["Sample"] = "This is a demo bill";
        byte[] pdf = PdfHelper.CreateReceiptPdf("JBVNL Bill", fields);
        Response.Clear(); Response.ContentType = "application/pdf"; Response.AddHeader("Content-Disposition","attachment; filename=Bill_"+id+".pdf"); Response.BinaryWrite(pdf); Response.End();
    }
}