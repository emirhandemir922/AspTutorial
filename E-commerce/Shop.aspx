<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="E_commerce.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Breadcrumb Section Begin -->
    <div class="breacrumb-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="breadcrumb-text">
                        <a href="#"><i class="fa fa-home"></i> Home</a>
                        <span>Shop</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumb Section Begin -->

    <!-- Product Shop Section Begin -->
    <section class="product-shop spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-6 col-sm-8 order-2 order-lg-1 produts-sidebar-filter">
                    <div class="filter-widget brand">
                        <h4 class="fw-title">Brand</h4>
                        <div class="fw-brand-check">
                            <div class="bc-item">
                                <div class="form-check">
                                    <input class="form-check-input brandMultipleCheckbox" type="checkbox" value=""/>
                                    <label class="form-check-label" for="flexCheckDefault" data-brand="SAMSUNG">SAMSUNG</label>
                                </div>
                            </div>
                            <div class="bc-item">
                                <div class="form-check">
                                    <input class="form-check-input brandMultipleCheckbox" type="checkbox" value=""/>
                                    <label class="form-check-label" for="flexCheckDefault" data-brand="Xiaomi">Xiaomi</label>
                                </div>
                            </div>
                            <div class="bc-item">
                                <div class="form-check">
                                    <input class="form-check-input brandMultipleCheckbox" type="checkbox" value=""/>
                                    <label class="form-check-label" for="flexCheckDefault" data-brand="Apple">APPLE</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="filter-widget price">
                        <h4 class="fw-title">Price</h4>
                        <div class="filter-range-wrap">
                            <div class="range-slider">
                                <div class="price-input">
                                    <input type="text" id="minamount" disabled>
                                    <input type="text" id="maxamount" disabled>
                                </div>
                            </div>
                            <div class="price-range ui-slider ui-corner-all ui-slider-horizontal ui-widget ui-widget-content"
                                data-min="200" data-max="24000">
                                <div id="price-slider" class="ui-slider-range ui-corner-all ui-widget-header" style="left: 50%; width: 50%;"></div>
                                <span tabindex="0" id="slider-min" class="ui-slider-handle ui-corner-all ui-state-default left"></span>
                                <span tabindex="0" id="slider-max" class="ui-slider-handle ui-corner-all ui-state-default right"></span>
                            </div>
                        </div>
                    </div>
                    <div class="filter-widget memory">
                        <h4 class="fw-title">Memory</h4>
                        <div class="form-check mb-2">
                            <input class="form-check-input memoryMultipleCheckbox" type="checkbox">
                            <label class="form-check-label" for="flexCheckDefault" data-memory="16">
                            16 GB
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input memoryMultipleCheckbox" type="checkbox" >
                            <label class="form-check-label" for="flexCheckDefault" data-memory="32">
                            32 GB
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input memoryMultipleCheckbox" type="checkbox">
                            <label class="form-check-label" for="flexCheckDefault" data-memory="64">
                            64 GB
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input memoryMultipleCheckbox" type="checkbox" >
                            <label class="form-check-label" for="flexCheckDefault" data-memory="128">
                            128 GB
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input memoryMultipleCheckbox" type="checkbox" >
                            <label class="form-check-label" for="flexCheckDefault" data-memory="256">
                            256 GB
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input memoryMultipleCheckbox" type="checkbox" >
                            <label class="form-check-label" for="flexCheckDefault" data-memory="512">
                            512 GB
                            </label>
                        </div>
                    </div>
                    <div class="filter-widget screen">
                        <h4 class="fw-title">Screen</h4>
                        <div class="form-check mb-2">
                            <input class="form-check-input screenCheckbox" type="checkbox" name="flexRadio">
                            <label class="form-check-label" for="flexRadioDefault2" data-minscreen="2.40" data-maxscreen="5.00">
                            2.40 inch - 5.00 inch
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input screenCheckbox" type="checkbox" name="flexRadio">
                            <label class="form-check-label" for="flexRadioDefault2" data-minscreen="5.1" data-maxscreen="6.00">
                            5.1 inch - 6.00 inch
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input screenCheckbox" type="checkbox" name="flexRadio">
                            <label class="form-check-label" for="flexRadioDefault2" data-minscreen="6.1" data-maxscreen="6.5">
                            6.1 inch - 6.5 inch
                            </label>
                        </div>
                        <div class="form-check mb-2">
                            <input class="form-check-input screenCheckbox" type="checkbox" name="flexRadio">
                            <label class="form-check-label" for="flexRadioDefault2" data-minscreen="6.5" data-maxscreen="99">
                            6.5 inch+
                            </label>
                        </div>
                    </div>
                    <div>
                        <button type="button" class="btn btn-outline-warning filterBtn">Filter</button>
                    </div>
                </div>
                <div class="col-lg-9 order-1 order-lg-2">
                    <div class="product-show-option">
                        <div class="row">
                            <div class="col-lg-8 col-md-8">
                                <div class="select-option">
                                    <select class="sorting">
                                        <option value="Default">Sort Products</option>
                                        <option value="PriceLH">Price (Low --> High)</option>
                                        <option value="PriceHL">Price (High --> Low)</option>
                                        <option value="Name">Name</option>
                                        <option value="Popularity">Popularity</option>
                                        <option value="HighSales">Top Selling</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
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
                                                <div class="sale pp-ad" style="margin-top: 26px;"><%# Eval("Sponsor") %></div>
                                                <div class="icon">
                                                    <button type="button" value="<%# Eval("Favorite") %>" class="btn icon_heart_alt"></button>
                                                </div>
                                                <ul>
                                                    <li class="w-icon active"><button type="button" class="btn btn-outline-warning addBagBtn"><i class="icon_bag_alt"></i></button></li>
                                                </ul>
                                            </div>
                                            <div class="pi-text">
                                                <div class="catagory-name">
                                                    <%# Eval("Category") %>
                                                    <br />
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
    <div class="container">
        <div class="d-flex justify-content-center mb-5">
            <asp:Label runat="server" ClientIDMode="Static" ID="ProductsCount"></asp:Label>
            <p class="mx-3">Product(s) has been found</p>
        </div>
        <nav aria-label="Page navigation example" class="d-flex justify-content-center">
            <ul class="pagination" style="font-size: 150%">
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-previous btn btn-dark" aria-label="Previous">
                        <span aria-hidden="true">&laquo;</span>
                        <span class="sr-only">Previous</span>
                    </button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-first btn btn-dark" aria-label="Previous">
                        First
                    </button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-numeric btn">1</button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-numeric btn">2</button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-numeric btn">3</button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-numeric btn">4</button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-numeric btn">5</button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-last btn btn-dark" aria-label="Previous">
                        Last
                    </button>
                </li>
                <li class="page-item mx-1 mb-2">
                    <button type="button" class="pagelink-next btn btn-dark" aria-label="Next">
                        <span aria-hidden="true">&raquo;</span>
                        <span class="sr-only">Next</span>
                    </button>
                </li>
            </ul>
        </nav>
    </div>
    <!-- Product Shop Section End -->

    <script type="text/javascript" src="/js/Shop.js"></script>
</asp:Content>
