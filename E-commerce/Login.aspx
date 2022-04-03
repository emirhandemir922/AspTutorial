<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="E_commerce.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Breadcrumb Section Begin -->
    <div class="breacrumb-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="breadcrumb-text">
                        <a href="HomePage.aspx"><i class="fa fa-home"></i> Home</a>
                        <span>Login</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumb Form Section Begin -->

    <!-- Login Section Begin -->
    <div class="register-login-section spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 offset-lg-3">
                    <div class="login-form">
                        <asp:Label ID="Header" runat="server" Text="Login"></asp:Label>
                        
                        <div class="group-input">
                            <label for="Mail"id="labelMail">Email address</label>
                            <input type="text" runat="server" class="form-control" id="loginMail">
                            <label class="text-danger" runat="server" id="mailError">Please enter a valid e-mail address</label>
                        </div>
                        <div class="group-input">
                            <label for="Pass" id="labelPassword">Password</label>
                            <input type="text" runat="server" class="form-control" id="loginPassword">
                            <label class="text-danger" runat="server" id="passwordError">Please enter a valid password</label>
                        </div>
                        <div class="group-input gi-check">
                            <div class="gi-more">
                                <label for="Save-Pass">
                                    Save Password
                                    <input type="checkbox" id="save-pass">
                                    <span class="checkmark"></span>
                                </label>
                                <a href="#" class="forget-pass">Forget your Password</a>
                            </div>
                        </div>
                        <asp:Button ID="loginButton" runat="server" class="site-btn login-btn" OnClick="loginButton_Click" Text="Sign In" />
                        <div class="switch-login">
                            <a href="Register.aspx" class="or-login">Or Create An Account</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Register Form Section End -->

    <script src="js/Login.js"></script>
</asp:Content>
