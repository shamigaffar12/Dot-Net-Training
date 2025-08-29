<%@ Page Title="Add Bill" Language="C#" AutoEventWireup="true" CodeBehind="AddBill.aspx.cs" Inherits="ElectricityBillProject.AddBill" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container mt-4">

    <h2 class="mb-3">Generate Bills</h2>

    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3 d-block"></asp:Label>

  
    <asp:Panel ID="pnlAskCount" runat="server" Visible="true" CssClass="mb-4">
      <div class="form-group row align-items-center">
        <label for="txtBillCount" class="col-sm-4 col-form-label">Enter number of bills to generate:</label>
        <div class="col-sm-2">
          <asp:TextBox ID="txtBillCount" runat="server" CssClass="form-control" />
        </div>
        <div class="col-sm-2">
          <asp:Button ID="btnSetCount" runat="server" Text="Set" CssClass="btn btn-primary" OnClick="btnSetCount_Click" />
        </div>
      </div>
    </asp:Panel>

   
    <asp:Panel ID="pnlInputBills" runat="server" Visible="false">

      <asp:Repeater ID="rptBills" runat="server">
        <ItemTemplate>
          <div class="card mb-3 shadow-sm">
            <div class="card-header font-weight-bold">
              Bill <%# Container.ItemIndex + 1 %>
            </div>
            <div class="card-body">
              <div class="form-row">
                <div class="form-group col-md-4">
                  <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" Placeholder="Consumer Number (e.g. EB12345)" />
                </div>
                <div class="form-group col-md-4">
                  <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Consumer Name" />
                </div>
                <div class="form-group col-md-4">
                  <asp:TextBox ID="txtUnits" runat="server" CssClass="form-control" Placeholder="Units Consumed" />
                </div>
              </div>
            </div>
          </div>
        </ItemTemplate>
      </asp:Repeater>

      <asp:Button ID="btnGenerateBills" runat="server" Text="Generate Bills" CssClass="btn btn-success mb-4" OnClick="btnGenerateBills_Click" />

      <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
    </asp:Panel>

  </div>
</asp:Content>
