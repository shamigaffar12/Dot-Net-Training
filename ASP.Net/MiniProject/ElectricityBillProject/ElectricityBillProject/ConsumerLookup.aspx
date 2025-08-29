<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumerLookup.aspx.cs" Inherits="ElectricityBillProject.ConsumerLookup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Find Your Electricity Bill Receipt</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h4>Enter Consumer Number</h4>
            </div>
            <div class="card-body">
                <asp:TextBox ID="txtConsumerNumber" runat="server" CssClass="form-control" Placeholder="Consumer Number"></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" Text="View Latest Receipt" CssClass="btn btn-success mt-3" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
</body>
</html>
