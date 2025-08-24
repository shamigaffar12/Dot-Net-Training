<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">Admin Login</h3>
<asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
<asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control mt-2" placeholder="Password"></asp:TextBox>
<asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary mt-3" Text="Login" OnClick="btnLogin_Click" /></div>
</asp:Content>