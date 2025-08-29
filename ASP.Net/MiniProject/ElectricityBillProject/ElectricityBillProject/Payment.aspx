<%@ Page Title="Make Payment" Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ElectricityBillProject.Payment" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>💳 Electricity Bill Payment</h2>

    <div style="max-width: 600px; padding: 20px; background-color: #f9f9f9; border-radius: 8px; box-shadow: 0 0 8px rgba(0,0,0,0.1);">
        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="DarkRed" /><br /><br />

        <asp:Label ID="lblConsumer" runat="server" Text="Consumer Number:" AssociatedControlID="txtConsumer" />
        <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" placeholder="e.g., EB12345" /><br />

        <asp:Label ID="lblAmount" runat="server" Text="Amount (₹):" AssociatedControlID="txtAmount" />
        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="e.g., 1500.00" /><br />

        <asp:Label ID="lblMethod" runat="server" Text="Payment Method:" AssociatedControlID="ddlMethod" />
        <asp:DropDownList ID="ddlMethod" runat="server" CssClass="form-control">
            <asp:ListItem Text="UPI" Value="UPI" />
            <asp:ListItem Text="QR" Value="QR" />
            <asp:ListItem Text="NetBanking" Value="NetBanking" />
        </asp:DropDownList><br />

        <asp:Button ID="btnGenQR" runat="server" Text="Generate QR" CssClass="btn btn-primary" OnClick="btnGenQR_Click" />
        <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" CssClass="btn btn-success" OnClick="btnPayNow_Click" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Payment" CssClass="btn btn-dark" OnClick="btnSubmit_Click" /><br /><br />

        <asp:Image ID="qrImage" runat="server" Width="150" Visible="false" /><br />
    </div>

    <hr />

    <h3>📄 Outstanding Bills</h3>
    <asp:GridView ID="gvOutstanding" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" EmptyDataText="No unpaid bills found.">
        <Columns>
            <asp:BoundField DataField="bill_id" HeaderText="Bill ID" />
            <asp:BoundField DataField="consumer_number" HeaderText="Consumer No." />
            <asp:BoundField DataField="consumer_name" HeaderText="Name" />
            <asp:BoundField DataField="units_consumed" HeaderText="Units" />
            <asp:BoundField DataField="bill_amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="bill_date" HeaderText="Bill Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="due_date" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
    </asp:GridView>
</asp:Content>
