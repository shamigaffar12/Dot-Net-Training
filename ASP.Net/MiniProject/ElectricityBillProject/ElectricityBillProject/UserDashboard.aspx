<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="ElectricityBillProject.UserDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2 class="text-primary">⚡ User Dashboard</h2>
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
            </div>

            <div class="row g-4">
                 <!-- Update Profile -->
                <div class="col-md-4">
                    <a href="Profile.aspx" class="btn btn-outline-warning w-100 p-3">
                        👤 Update Profile
                    </a>
                </div>
                <!-- View Bill -->
                <div class="col-md-4">
                    <a href="ViewBill.aspx" class="btn btn-outline-primary w-100 p-3">
                        📄 View Bill
                    </a>
                </div>

                <!-- Pay Bill -->
                <div class="col-md-4">
                    <a href="Payment.aspx" class="btn btn-outline-success w-100 p-3">
                        💰 Pay Bill
                    </a>
                </div>

                <!-- Transaction History -->
                <div class="col-md-4">
                    <a href="BillReceipt.aspx" class="btn btn-outline-info w-100 p-3">
                        📑 Bill Receipt
                    </a>
                </div>

                <!-- Connection Info -->
                <div class="col-md-4">
                    <a href="ConnectionInfo.aspx" class="btn btn-outline-secondary w-100 p-3">
                        🔌 Connection Info
                    </a>
                </div>

                <!-- New  Profile -->
                <div class="col-md-4">
                    <a href="UserRegistration.aspx" class="btn btn-outline-warning w-100 p-3">
                        👤 New User Register
                    </a>
                </div>

                <!-- Raise Concern -->
                <div class="col-md-4">
                    <a href="RaiseConcern.aspx" class="btn btn-outline-danger w-100 p-3">
                        ❓ Raise Concern
                    </a>
                </div>

                <!-- Apply for New Connection -->
                <div class="col-md-4">
                    <a href="NewConnection.aspx" class="btn btn-outline-dark w-100 p-3">
                        📝 Apply New Connection
                    </a>
                </div>

                <!-- Bill Print -->
                <div class="col-md-4">
                    <a href="BillPrint.aspx" class="btn btn-outline-primary w-100 p-3">
                        🖨️ Print Bill
                    </a>
                </div>

                <!-- User Home -->
                <div class="col-md-4">
                    <a href="UserDashboard.aspx" class="btn btn-outline-secondary w-100 p-3">
                        🏠 User Home
                    </a>
                </div>

                <!-- Back -->
                <div class="col-md-4">
                    <asp:Button ID="btnBack" runat="server" Text="⬅️ Back" CssClass="btn btn-outline-secondary w-100 p-3" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
