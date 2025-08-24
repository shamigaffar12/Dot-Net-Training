using System; using System.IO; using System.Text; using System.Collections.Generic;
public static class PdfHelper {
    public static byte[] CreateReceiptPdf(string title, Dictionary<string,string> fields) {
        // Simple HTML -> bytes fallback (works as .pdf in many browsers)
        StringBuilder sb = new StringBuilder();
        sb.Append("<html><body style='font-family:Segoe UI,Arial'><h2>"+System.Net.WebUtility.HtmlEncode(title)+"</h2><table style='width:100%;border-collapse:collapse;'>");
        foreach(var kv in fields){ sb.AppendFormat("<tr><td style='padding:6px;border:1px solid #eee;font-weight:600'>{0}</td><td style='padding:6px;border:1px solid #eee'>{1}</td></tr>", System.Net.WebUtility.HtmlEncode(kv.Key), System.Net.WebUtility.HtmlEncode(kv.Value)); }
        sb.Append("</table></body></html>"); return Encoding.UTF8.GetBytes(sb.ToString());
    }
}