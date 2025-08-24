<%@ Page Title="Add Bill" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AddBill.aspx.cs" Inherits="ElectricityBillProject.AddBill" %>

<asp:Content ContentPlaceHolderID="SidebarLinks" runat="server">
  <li class="nav-item">
    <a class="nav-link" href="AddBill.aspx">
      <i class="fas fa-file-invoice-dollar"></i>
      <span>Add Bill</span>
    </a>
  </li>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
  <div class="container-fluid mt-4">
    <div class="card shadow mb-4" style="max-width: 600px;">
      <div class="card-header py-3">
        <h6 class="m-0 font-weight-bold text-primary">Add New Electricity Bill</h6>
      </div>
      <div class="card-body">
        <div class="form-group">
          <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" placeholder="Consumer Number" />
        </div>
        <div class="form-group">
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Consumer Name" />
        </div>
        <div class="form-group">
          <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control" placeholder="Units Consumed" />
        </div>
        <div class="form-group text-end">
          <asp:Button ID="btnGen" runat="server" Text="Generate Bill" CssClass="btn btn-primary" OnClick="btnGen_Click" />
        </div>
        <asp:Label ID="lblMsg" runat="server" CssClass="text-danger" />
      </div>
    </div>

    <div class="card shadow mt-4">
      <div class="card-header">
        <h6 class="m-0 font-weight-bold text-secondary">Latest Bills</h6>
      </div>
      <div class="card-body">
        <div class="input-group mb-3" style="max-width: 200px;">
          <asp:TextBox ID="txtN" runat="server" CssClass="form-control" Text="5" />
          <div class="input-group-append">
            <asp:Button ID="btnFetch" runat="server" Text="Fetch" CssClass="btn btn-outline-secondary" OnClick="btnFetch_Click" />
          </div>
        </div>
        <asp:GridView ID="grid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="true" />
      </div>
    </div>
  </div>
</asp:Content>
