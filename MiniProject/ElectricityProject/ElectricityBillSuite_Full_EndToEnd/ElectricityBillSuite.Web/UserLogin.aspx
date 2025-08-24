<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="UserLogin" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">User Login</h3>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
<asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control mt-2" placeholder="Password"></asp:TextBox>
<asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary mt-3" Text="Login" OnClick="btnLogin_Click" />
<asp:Literal ID="litMsg" runat="server" /></div>
</asp:Content>