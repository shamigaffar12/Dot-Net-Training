<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBill.aspx.cs" Inherits="ElectricityBillProject.ViewBill" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Electricity Bills</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
       
        .card-header {
            font-weight: bold;
            font-size: 1.25rem;
        }
        .form-label {
            font-weight: 600;
        }
        .grid-empty {
            text-align: center;
            color: #888;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-success text-white">
                <h4>Search Electricity Bills</h4>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <label for="txtConsumerNumber" class="form-label">Consumer Number</label>
                    <asp:TextBox ID="txtConsumerNumber" runat="server" CssClass="form-control" Placeholder="Enter Consumer Number"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="ddlStatus" class="form-label">Bill Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                        <asp:ListItem Value="All" Text="All Bills" Selected="True" />
                        <asp:ListItem Value="Paid" Text="Paid Bills" />
                        <asp:ListItem Value="Unpaid" Text="Unpaid Bills" />
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label for="txtN" class="form-label">Number of Bills to Show</label>
                    <asp:TextBox ID="txtN" runat="server" CssClass="form-control" Placeholder="Default 5"></asp:TextBox>
                </div>

                <asp:Button ID="btnGo" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnGo_Click" />

                <asp:Label ID="lblMsg" runat="server" CssClass="mt-3 d-block"></asp:Label>

                <asp:GridView ID="grid" runat="server" CssClass="table table-bordered table-striped mt-3" AutoGenerateColumns="false" EmptyDataText="No bills found.">
                    <Columns>
                        <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
                        <asp:BoundField DataField="consumer_name" HeaderText="Consumer Name" />
                        <asp:BoundField DataField="units_consumed" HeaderText="Units Consumed" />
                        <asp:BoundField DataField="bill_amount" HeaderText="Bill Amount (Rs.)" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="bill_date" HeaderText="Bill Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
