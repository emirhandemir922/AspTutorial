<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchProducts.aspx.cs" Inherits="E_commerce.WebForm14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<section class="product-shop spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-9 order-1 order-lg-2">
                    <div class="product-list">
                        <!-- Modal -->
                        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <img id="popupImg" style="width: 350px; height: 600px; object-fit: cover; margin-left: 50px; margin-right: 50px;"/>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <asp:Repeater ID="ProductsRepeater" runat="server">
                                <HeaderTemplate></HeaderTemplate>
                                <ItemTemplate>
                                    <div class="col-lg-4 col-sm-6 product <%# Eval("id") %>">
                                        <div class="product-item" data-id="<%# Eval("id") %>">
                                            <div class="pi-pic" >
                                                <img src="<%# Eval("Photo") %>" alt="<%# Eval("ProductName") %>" class="productImg" style="width: 200px; height: 300px; object-fit: cover;">
                                                <div class="sale pp-sale"></div>
                                                <ul>
                                                    <li class="quick-view"><button type="button" class="btn btn-primary quickViewBtn" data-toggle="modal" data-target="#exampleModal">+Quick View</button></li>
                                                </ul>
                                            </div>
                                            <div class="pi-text">
                                                <div class="catagory-name">
                                                    <h6 class="font-weight-light productName" style="cursor: pointer;"><%# Eval("ProductName") %></h6>
                                                </div>
                                                <a href="#">
                                                    <h5></h5>
                                                </a>
                                                <div class="product-price">
                                                    <%# Eval("Price") %> TL
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
    <script type="text/javascript" src="/js/ViewCard.js"></script>
</asp:Content>
