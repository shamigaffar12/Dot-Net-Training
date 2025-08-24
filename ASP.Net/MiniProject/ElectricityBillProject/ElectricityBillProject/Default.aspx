<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" CodeBehind="Default.aspx.cs" Inherits="ElectricityBillProject.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Banner Section -->
    <div class="banner fade-in">
        <h2>Utility Bill Payment & Services</h2>
        <p>Fast, simple and secure. Pay bills, view history, and manage connections.</p>
    </div>

    <!-- Image Carousel -->
    <div class="carousel-container fade-in">
        <div class="carousel">
            <img src="https://images.unsplash.com/photo-1504384308090-c894fdcc538d?auto=format&fit=crop&w=1200&q=80" class="carousel-image" />
            <img src="https://images.unsplash.com/photo-1509475826633-fed577a2c71b?auto=format&fit=crop&w=1200&q=80" class="carousel-image" />
            <img src="https://images.unsplash.com/photo-1501594907352-04cda38ebc29?auto=format&fit=crop&w=1200&q=80" class="carousel-image" />
        </div>
    </div>

    <!-- Action Cards -->
    <div class="two-col">
        <div class="card fade-in">
            <h3>Quick Actions</h3>
            <asp:HyperLink CssClass="action-link" NavigateUrl="UserLogin.aspx" Text="User Login" runat="server" />
            <br />
            <asp:HyperLink CssClass="action-link" NavigateUrl="UserRegistration.aspx" Text="New User? Register" runat="server" />
            <br />
            <asp:HyperLink CssClass="action-link" NavigateUrl="Payment.aspx" Text="Pay Bill (UPI/QR)" runat="server" />
        </div>

        <div class="card fade-in">
            <h3>Admin</h3>
            <asp:HyperLink CssClass="action-link" NavigateUrl="AdminLogin.aspx" Text="Admin Login" runat="server" />
            <br />
        </div>
    </div>

    <!-- Custom Styles -->
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #f5f7fa;
        }

        .banner {
            background: linear-gradient(135deg, #0072ff, #00c6ff);
            color: white;
            padding: 60px 30px;
            text-align: center;
            border-radius: 12px;
            box-shadow: 0 8px 30px rgba(0, 114, 255, 0.4);
            margin-bottom: 40px;
        }

        .banner h2 {
            font-size: 2.8rem;
            font-weight: 700;
            margin-bottom: 12px;
            letter-spacing: 1.5px;
            text-transform: uppercase;
        }

        .banner p {
            font-size: 1.3rem;
            max-width: 600px;
            margin: 0 auto;
            line-height: 1.5;
        }

        /* Carousel */
        .carousel-container {
            width: 100%;
            max-width: 900px;
            margin: 0 auto 40px;
            overflow: hidden;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
        }

        .carousel {
            display: flex;
            width: 300%;
            animation: slideCarousel 15s infinite;
        }

        .carousel-image {
            width: 100%;
            object-fit: cover;
            height: 300px;
            flex-shrink: 0;
        }

        @keyframes slideCarousel {
            0%, 33% {
                transform: translateX(0%);
            }
            34%, 66% {
                transform: translateX(-100%);
            }
            67%, 100% {
                transform: translateX(-200%);
            }
        }

        .two-col {
            display: flex;
            justify-content: center;
            gap: 30px;
            flex-wrap: wrap;
            padding: 20px;
        }

        .card {
            background: white;
            border-radius: 14px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            padding: 25px 35px;
            width: 320px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            text-align: center;
        }

        .card:hover {
            transform: translateY(-8px);
            box-shadow: 0 20px 40px rgba(0, 114, 255, 0.35);
        }

        .card h3 {
            margin-bottom: 20px;
            color: #0072ff;
            font-size: 1.8rem;
            font-weight: 700;
        }

        .action-link {
            display: block;
            color: #0072ff;
            font-weight: 600;
            margin-bottom: 12px;
            text-decoration: none;
            font-size: 1.1rem;
            transition: color 0.3s ease;
        }

        .action-link:hover {
            color: #004a99;
            text-decoration: underline;
        }

        /* Animations */
        .fade-in {
            opacity: 0;
            animation: fadeIn ease-in-out 1s forwards;
        }

        .fade-in:nth-child(2) {
            animation-delay: 0.5s;
        }

        .fade-in:nth-child(3) {
            animation-delay: 0.8s;
        }

        @keyframes fadeIn {
            to {
                opacity: 1;
            }
        }

        /* Responsive */
        @media (max-width: 768px) {
            .carousel-image {
                height: 200px;
            }

            .card {
                width: 90%;
                margin-bottom: 20px;
            }

            .banner h2 {
                font-size: 2rem;
            }

            .banner p {
                font-size: 1rem;
            }
        }
    </style>
</asp:Content>
