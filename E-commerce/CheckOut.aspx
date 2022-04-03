<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="E_commerce.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumb Section Begin -->
    <div class="breacrumb-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="breadcrumb-text product-more">
                        <a href="HomePage.aspx"><i class="fa fa-home"></i> Home</a>
                        <a href="Shop.aspx">Shop</a>
                        <span>Check Out</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumb Section Begin -->

    <!-- Shopping Cart Section Begin -->
        <div class="container">
            <div class="row">
                <div class="col">
                    <h3 class="mt-5">Billing Details</h3>
                    <div class="row mt-5">
                        <div class="col">
                            <label class="form-label">First Name*</label>
                            <input class="form-control" type="text" id="FirstName"/>
                        </div>
                        <div class="col">
                            <label class="form-label">Last Name*</label>
                            <input class="form-control" type="text" id="LastName"/>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <label class="form-label">Company Name</label>
                        <input class="form-control" type="text" class="" id="CompanyName"/>
                    </div>
                    <div class="row mt-2">
                        <label class="form-label">Country</label>
                        <select class="form-control">
                            <option>Turkey</option>
                            <option>Germany</option>
                            <option>USA</option>
                        </select>
                    </div>
                    <div class="row mt-2">
                        <label class="form-label">Street Address</label>
                        <textarea class="form-control" id="Address"></textarea>
                    </div>
                    <div class="row mt-2">
                        <label class="form-label">Postcode/ZIP</label>
                        <input class="form-control" id="Postcode"></input>
                    </div>
                    <div class="row mt-2">
                        <label class="form-label">Town/City</label>
                        <input class="form-control" id="City"></input>
                    </div>
                    <div class="row mt-2 mb-5">
                        <div class="col">
                            <label class="form-label">Email Address*</label>
                            <input class="form-control" type="text" id="Email"/>
                        </div>
                        <div class="col">
                            <label class="form-label">Phone*</label>
                            <input class="form-control" type="text" id="Phone"/>
                        </div>
                    </div>
                </div>

                <div class="col" style="margin-left: 10%;">
                    <h3 class="mt-5">Your Orders</h3>
                    <table class="table mt-5">
                        <thead>
                            <tr>
                                <th class="text-center" scope="col">Quantity</th>
                                <th class="text-center" scope="col">Productname</th>
                                <th class="text-center" scope="col" class="float-right">Price</th>
                            </tr>
                        </thead>
                        <tbody id="CheckOutTable">
                            <asp:Repeater ID="ShoppingCartRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center"><%#Eval("Quantity") %></td>
                                        <td class="text-center"><%#Eval("ProductName") %></td>
                                        <td class="text-center">$<%#Eval("Price") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td class="text-center">-</td>
                                <td class="text-warning text-center">TOTAL</td>
                                <td class="text-warning text-center" id="Total" runat="server"></td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Button ID="OrderBtn" runat="server" class="site-btn btn-warning" OnClick="OrderButton_Click" Text="PLACE ORDER" />
                </div>
            </div>
        </div>
    <!-- Shopping Cart Section End -->

</asp:Content>
