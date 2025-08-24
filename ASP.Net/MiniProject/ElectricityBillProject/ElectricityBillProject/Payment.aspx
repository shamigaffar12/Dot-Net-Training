<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ElectricityBillProject.Payment" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Portal - JBVNL</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .receipt-section {
            border: 1px solid #28a745;
            padding: 20px;
            border-radius: 8px;
            background-color: #eafaf1;
        }
    </style>
    <script>
        function printReceipt() {
            var printContents = document.getElementById("receiptBox").innerHTML;
            var original = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = original;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h4>💳 Pay Your Electricity Bill</h4>
            </div>
            <div class="card-body">

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3 d-block"></asp:Label>

                <div class="mb-3">
                    <label for="txtConsumer" class="form-label">Consumer Number</label>
                    <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" Placeholder="Enter your consumer number"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtAmount" class="form-label">Amount (INR)</label>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Placeholder="Enter amount to pay"></asp:TextBox>
                </div>

                <asp:Button ID="btnGenerateQR" runat="server" Text="Generate  QR" CssClass="btn btn-info me-2" OnClick="btnGenerateQR_Click" />
                <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm Payment" CssClass="btn btn-success" OnClick="btnConfirmPayment_Click" />

                <div id="qrSection" runat="server" class="mt-4" style="display:none;">
                    <h5>Scan QR to Pay</h5>
                    <img src="~/Styles/myQR.png" alt="Scan to Pay" width="200" />
                    <p class="mt-2 text-muted">Scan with UPI app and enter the exact amount shown above.</p>
                </div>

                <div id="receiptSection" runat="server" class="receipt-section mt-4" style="display:none;">
                    <h4>🧾 Payment Receipt</h4>
                    <div id="receiptBox">
                        <asp:Literal ID="litReceipt" runat="server" />
                    </div>
                    <div class="mt-3">
                        <button type="button" class="btn btn-outline-primary me-2" onclick="printReceipt()">🖨️ Print</button>
                        <asp:Button ID="btnEmailPDF" runat="server" Text="📧 Email & Download PDF" CssClass="btn btn-outline-success" OnClick="btnEmailPDF_Click" />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
