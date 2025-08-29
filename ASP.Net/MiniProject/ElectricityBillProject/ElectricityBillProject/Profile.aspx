<%@ Page Title="Profile" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ElectricityBillProject.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Profile - EBilling
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">
                <div class="card shadow-sm border-0">
                    <div class="card-body">
                        <h4 class="card-title mb-4 text-primary">
                            <i class="bi bi-person-circle me-2"></i>My Profile
                        </h4>

                        
                        <div class="mb-3">
                            <label for="txtName" class="form-label">Full Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        </div>

                   
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        </div>

                       
                        <div class="mb-3">
                            <label for="txtPhone" class="form-label">Phone Number</label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" TextMode="Phone" />
                        </div>

                       
                        <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" CssClass="btn btn-primary w-100" OnClick="btnUpdate_Click" />

                        
                        <asp:Label ID="lblMsg" runat="server" CssClass="d-block mt-3 fw-bold"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
