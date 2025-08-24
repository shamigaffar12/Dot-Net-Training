<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateBill.aspx.cs" Inherits="GenerateBill" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">Generate Bill</h3>
<asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" placeholder="Consumer Number"></asp:TextBox>
<asp:TextBox ID="txtUnits" runat="server" CssClass="form-control mt-2" placeholder="Units"></asp:TextBox>
<asp:Button ID="btnGen" runat="server" CssClass="btn btn-primary mt-2" Text="Generate" OnClick="btnGen_Click" />
<asp:Literal ID="litMsg" runat="server"></asp:Literal></div></asp:Content>