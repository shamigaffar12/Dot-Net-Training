<%@ Page Title="Dashboard" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ElectricityBillProject.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container-fluid">
    <h1 class="h3 mb-4 text-gray-800">Dashboard</h1>


    <div class="row">
      <div class="col-xl-3 mb-4">
        <div class="card border-left-warning shadow h-100 py-2">
          <div class="card-body">
            <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">Total Due</div>
            <asp:Label ID="lblDue" runat="server" CssClass="h5 mb-0 font-weight-bold text-gray-800" Text="₹0"></asp:Label>
          </div>
        </div>
      </div>

      <div class="col-xl-3 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
          <div class="card-body">
            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Last Payment</div>
            <asp:Label ID="lblLastPay" runat="server" CssClass="h5 mb-0 font-weight-bold text-gray-800" Text="--"></asp:Label>
          </div>
        </div>
      </div>
    </div>


    <h3>Your Connections</h3>
    <asp:GridView ID="gvConnections" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None" EmptyDataText="No connections found.">
      <Columns>
        <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
        <asp:BoundField DataField="connection_address" HeaderText="Address" />
        <asp:BoundField DataField="connection_type" HeaderText="Type" />
        <asp:BoundField DataField="connection_status" HeaderText="Status" />
      </Columns>
    </asp:GridView>

    <br />


    <h3>Your Bills</h3>
    <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="False" OnRowCommand="gvBills_RowCommand" CssClass="table table-bordered table-striped" GridLines="None" EmptyDataText="No bills found.">
      <Columns>
        <asp:BoundField DataField="consumer_number" HeaderText="Consumer No" />
        <asp:BoundField DataField="consumer_name" HeaderText="Name" />
        <asp:BoundField DataField="units_consumed" HeaderText="Units" />
        <asp:BoundField DataField="bill_amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="bill_date" HeaderText="Bill Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="due_date" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="status" HeaderText="Status" />
        <asp:ButtonField Text="Download PDF" CommandName="DownloadPdf" ButtonType="Button" CommandArgument='<%# Eval("bill_id") %>' />
        <asp:ButtonField Text="Print" CommandName="Print" ButtonType="Button" CommandArgument='<%# Eval("bill_id") %>' />
        <asp:ButtonField Text="Email Receipt" CommandName="EmailReceipt" ButtonType="Button" CommandArgument='<%# Eval("bill_id") %>' />
      </Columns>
    </asp:GridView>

    <br />


    <h3>Payment Transactions</h3>
    <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None" EmptyDataText="No transactions found.">
      <Columns>
        <asp:BoundField DataField="payment_id" HeaderText="Payment ID" />
        <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
        <asp:BoundField DataField="amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="payment_date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
        <asp:BoundField DataField="method" HeaderText="Method" />
        <asp:BoundField DataField="status" HeaderText="Status" />
        <asp:BoundField DataField="txn_ref" HeaderText="Transaction Reference" />
      </Columns>
    </asp:GridView>

    <br />

  
    <h3>Your Concerns</h3>
    <asp:GridView ID="gvConcerns" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" GridLines="None" EmptyDataText="No concerns found.">
      <Columns>
        <asp:BoundField DataField="concern_id" HeaderText="Concern ID" />
        <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
        <asp:BoundField DataField="message" HeaderText="Message" />
        <asp:BoundField DataField="status" HeaderText="Status" />
        <asp:BoundField DataField="created_at" HeaderText="Created At" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
        <asp:BoundField DataField="resolved_at" HeaderText="Resolved At" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
      </Columns>
    </asp:GridView>

    <br /><hr />

    
    <h3>Update Profile</h3>
    <asp:Label ID="lblProfileMsg" runat="server" ForeColor="Green"></asp:Label><br />

    <asp:TextBox ID="txtName" runat="server" Placeholder="Name" CssClass="form-control mb-2" />
    <asp:TextBox ID="txtPhone" runat="server" Placeholder="Phone" CssClass="form-control mb-2" />
    <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email" CssClass="form-control mb-2" />
    <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-control mb-2" />

    <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdateProfile_Click" />

    <br /><br />


    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />

    <br /><br />

   
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
  </div>
</asp:Content>
