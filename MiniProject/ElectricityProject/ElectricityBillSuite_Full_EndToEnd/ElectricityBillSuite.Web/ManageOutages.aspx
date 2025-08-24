<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageOutages.aspx.cs" Inherits="ManageOutages" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">Manage Outages</h3>
<asp:TextBox ID="txtLoc" runat="server" CssClass="form-control" placeholder="Location"></asp:TextBox>
<asp:TextBox ID="txtStatus" runat="server" CssClass="form-control mt-2" placeholder="Status"></asp:TextBox>
<asp:TextBox ID="txtStart" runat="server" CssClass="form-control mt-2" placeholder="Start (yyyy-mm-dd hh:mm)"></asp:TextBox>
<asp:TextBox ID="txtEnd" runat="server" CssClass="form-control mt-2" placeholder="End (yyyy-mm-dd hh:mm)"></asp:TextBox>
<asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary mt-2" Text="Add" OnClick="btnAdd_Click" />
<asp:Literal ID="litOut" runat="server"></asp:Literal></div></asp:Content>