<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="E_commerce.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Breadcrumb Section Begin -->
    <div class="breacrumb-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="breadcrumb-text">
                        <a href="#"><i class="fa fa-home"></i> Home</a>
                        <span>Register</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumb Form Section Begin -->

    <!-- Register Section Begin -->
    <div class="register-login-section spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 offset-lg-3">
                    <div class="register-form">
                        <asp:Label ID="Header" runat="server" Text="Register"></asp:Label>
                        
                        <div class="group-input">
                            <label for="name">Name</label>
                            <input type="text" runat="server" id="registerName">
                            <label class="text-danger" id="nameError" runat="server">Please enter a valid name (Make sure to use ENG characters only.)</label>
                        </div>
                        <div class="group-input">
                            <label for="surname">Surname</label>
                            <input type="text" runat="server" id="registerSurName" />
                            <label class="text-danger" id="surNameError" runat="server">Please enter a valid surname (Make sure to use ENG characters only.)</label>
                        </div>
                        <div class="group-input">
                            <label for="email">Email address</label>
                            <input type="text" runat="server" id="registerMail">
                            <label class="text-danger" id="mailError" runat="server">Please enter a valid e-mail (Make sure to use ENG characters)</label>
                        </div>
                        <div class="group-input">
                            <label for="pass">Password</label>
                            <input type="text" runat="server" id="registerPassword">
                            <label class="text-danger" id="passwordError" runat="server">Password must be at least 8 characters long and it should contain at least one digit, one lower and one uppercase characters </label>
                        </div>
                        <asp:Button ID="registerButton" OnClick="registerButton_Click" runat="server" class="site-btn register-btn mt-5" Text="Register" />
                        <div class="switch-login">
                            <a href="Login.aspx" class="or-login">Or Login</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Register Form Section End -->

    <script src="js/Register.js"></script>
</asp:Content>
