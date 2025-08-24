<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" MasterPageFile="~/Site.master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3 class="section-title">Payment</h3>
<input id="txtConsumer" class="form-control" placeholder="Consumer No" />
<input id="txtAmount" class="form-control mt-2" placeholder="Amount" />
<button class="btn btn-primary mt-2" onclick="openRazor()">Pay (Test)</button>
<p class="small mt-2">Razorpay test mode. Webhook endpoint: /Webhook.aspx</p>
</div>
<script src="https://checkout.razorpay.com/v1/checkout.js"></script>
<script>
function openRazor(){ var amt = parseFloat($('#txtAmount').val())||0; var options = { key: 'rzp_test_1234567890abcdef', amount: Math.round(amt*100), currency:'INR', name:'JBVNL', description:'Demo', handler:function(resp){ window.location='PaymentSuccess.aspx?pid='+resp.razorpay_payment_id+'&amt='+amt+'&cons='+encodeURIComponent($('#txtConsumer').val()); } }; var r = new Razorpay(options); r.open(); }
</script>
</asp:Content>