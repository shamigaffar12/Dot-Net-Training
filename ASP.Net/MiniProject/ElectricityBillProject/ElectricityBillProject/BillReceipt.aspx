<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillReceipt.aspx.cs" Inherits="ElectricityBillProject.BillReceipt" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill Receipt</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h4 class="mb-0">Electricity Bill Receipt</h4>
                </div>
                <div class="card-body bg-light">
                    <asp:Literal ID="litReceipt" runat="server"></asp:Literal>
                    <asp:Button ID="btnDownloadPdf" runat="server" Text="Download PDF"
                        CssClass="btn btn-success mt-4" OnClick="btnDownloadPdf_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
