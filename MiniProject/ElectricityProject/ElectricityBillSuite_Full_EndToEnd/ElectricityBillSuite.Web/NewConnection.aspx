<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewConnection.aspx.cs" Inherits="NewConnection" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">Apply for New Connection</h3>
<asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mt-2" placeholder="Email"></asp:TextBox>
<asp:TextBox ID="txtPhone" runat="server" CssClass="form-control mt-2" placeholder="Phone"></asp:TextBox>
<asp:TextBox ID="txtAddress" runat="server" CssClass="form-control mt-2" placeholder="Address"></asp:TextBox>
<asp:DropDownList ID="ddlLocality" runat="server" CssClass="form-control mt-2"><asp:ListItem>Urban</asp:ListItem><asp:ListItem>Rural</asp:ListItem></asp:DropDownList>
<input type="file" name="idProof" class="form-control mt-2" />
<input type="file" name="photo" class="form-control mt-2" />
<asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary mt-3" Text="Submit (₹8000)" OnClick="btnApply_Click" />
</div></asp:Content>