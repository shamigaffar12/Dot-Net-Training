<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaiseConcern.aspx.cs" Inherits="RaiseConcern" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">Raise Concern</h3>
<asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject"></asp:TextBox>
<asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" CssClass="form-control mt-2" placeholder="Describe"></asp:TextBox>
<asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary mt-2" Text="Submit" OnClick="btnSend_Click" /></div></asp:Content>