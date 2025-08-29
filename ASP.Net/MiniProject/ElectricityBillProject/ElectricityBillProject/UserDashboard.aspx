<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="ElectricityBillProject.UserDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f9f9f9;
        }
        .card-header {
            font-weight: bold;
            font-size: 1.3rem;
            background: linear-gradient(45deg, #4e73df, #224abe);
            color: white;
        }
        .btn-custom {
            min-width: 120px;
        }
        .label-success {
            color: #155724;
            background-color: #d4edda;
            border-radius: 5px;
            padding: 2px 6px;
            font-weight: bold;
        }
        .label-danger {
            color: #721c24;
            background-color: #f8d7da;
            border-radius: 5px;
            padding: 2px 6px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2 class="text-primary">⚡ User Dashboard</h2>
            </div>

            <asp:Literal ID="litMessage" runat="server"></asp:Literal>

           
            <div class="card shadow mb-5">
                <div class="card-header">🔍 View & Manage Your Electricity Bill</div>
                <div class="card-body">
                    <div class="row g-3 mb-3">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtConsumerNumber" runat="server" CssClass="form-control" placeholder="Enter Consumer Number"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtConsumerName" runat="server" CssClass="form-control" placeholder="Enter Consumer Name (optional)"></asp:TextBox>
                        </div>
                        <div class="col-md-4 d-flex gap-2 flex-wrap">
                            <asp:Button ID="btnPrintBill" runat="server" Text="🖨 Print Bill" CssClass="btn btn-success btn-custom" OnClick="btnPrintBill_Click" />
                            <asp:Button ID="btnDownloadBill" runat="server" Text="⬇ Download Bill" CssClass="btn btn-primary btn-custom" OnClick="btnDownloadBill_Click" />
                            <asp:Button ID="btnEmailBill" runat="server" Text="✉ Email Bill" CssClass="btn btn-warning btn-custom" OnClick="btnEmailBill_Click" />
                        </div>
                    </div>
                    <asp:Label ID="lblBillMsg" runat="server" CssClass="fw-bold"></asp:Label>
                </div>
            </div>

           
            <asp:Panel ID="pnlTransactions" runat="server" Visible="false" CssClass="mt-4">
                <h4>📜 Transaction History (Bills, Payments & Concerns)</h4>
                <asp:GridView ID="gvTransactions" runat="server" CssClass="table table-bordered table-striped"
                    AutoGenerateColumns="false" OnRowCommand="gvTransactions_RowCommand" EmptyDataText="No transactions found.">
                    <Columns>
                        <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:dd MMM yyyy HH:mm}" SortExpression="TransactionDate" />
                        <asp:BoundField DataField="TransactionType" HeaderText="Type" />
                        <asp:BoundField DataField="Details" HeaderText="Details" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnDownloadReceipt" runat="server" Text="Download Receipt" CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="DownloadReceipt" CommandArgument='<%# Eval("TransactionId") + "|" + Eval("TransactionType") %>'
                                    Visible='<%# Eval("TransactionType").ToString() == "Payment" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

 
            <div class="row g-3 mt-4">
                <div class="col-md-4">
                    <a href="Profile.aspx" class="btn btn-outline-warning w-100 p-3">👤 Update Profile</a>
                </div>
                <div class="col-md-4">
                    <a href="ViewBill.aspx" class="btn btn-outline-primary w-100 p-3">📄 View Bill</a>
                </div>
                <div class="col-md-4">
                    <a href="Payment.aspx" class="btn btn-outline-success w-100 p-3">💰 Pay Bill</a>
                </div>
                <div class="col-md-4">
                    <a href="ConsumerLookup.aspx" class="btn btn-outline-info w-100 p-3">📑 Bill Receipt</a>
                </div>
               <div class="col-md-4">
                    <asp:Button ID ="btnViewConnections" runat="server" Text="🔌 View Connection Info" CssClass="btn btn-outline-secondary w-100 p-3" OnClick="btnViewConnections_Click" />
                </div> 
                <div class="col-md-4">
                    <a href="UserRegistration.aspx" class="btn btn-outline-warning w-100 p-3">👥 New User Register</a>
                </div>
                <div class="col-md-4">
                    <a href="RaiseConcern.aspx" class="btn btn-outline-danger w-100 p-3">❓ Raise Concern</a>
                </div>
                <div class="col-md-4">
                    <a href="NewConnection.aspx" class="btn btn-outline-dark w-100 p-3">📝 Apply New Connection</a>
                </div>
                <!--<div class="col-md-4">
                    <a href="BillReceiptPdf.aspx" class="btn btn-outline-primary w-100 p-3">🖨️ Print All Bills</a>
                </div>-->
                <div class="col-md-4">
                    <a href="UserDashboard.aspx" class="btn btn-outline-secondary w-100 p-3">🏠 Home</a>
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnBack" runat="server" Text="⬅️ Back" CssClass="btn btn-outline-secondary w-100 p-3" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
