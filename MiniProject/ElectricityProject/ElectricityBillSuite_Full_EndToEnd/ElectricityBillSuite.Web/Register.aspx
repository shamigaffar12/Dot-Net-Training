<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">User Registration</h3>
<asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Full name"></asp:TextBox>
<asp:TextBox ID="txtPhone" runat="server" CssClass="form-control mt-2" placeholder="Phone"></asp:TextBox>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mt-2" placeholder="Email"></asp:TextBox>
<asp:TextBox ID="txtDob" runat="server" CssClass="form-control mt-2" placeholder="DOB (yyyy-mm-dd)"></asp:TextBox>
<asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control mt-2" placeholder="Password"></asp:TextBox>
<asp:Button ID="btnReg" runat="server" CssClass="btn btn-primary mt-3" Text="Register" OnClick="btnReg_Click" />
<asp:Literal ID="litMsg" runat="server" /></div>
</asp:Content>