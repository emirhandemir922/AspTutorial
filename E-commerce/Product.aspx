<%@ Page Title="Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Product.aspx.cs" Inherits="E_commerce.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="js/Product.js"></script>
    <!-- Breadcrumb Section Begin -->
    <div class="breacrumb-section">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="breadcrumb-text product-more">
                        <a href="HomePage.aspx"><i class="fa fa-home"></i> Home</a>
                        <a href="#">Shop</a>
                        <span>Detail</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumb Section Begin -->
 
    <!-- Product Shop Section Begin -->
    <section class="product-shop spad page-details">
        <div class="container"> 
                <div class="col-lg-9">
                    <div class="row">
                            <div class="col-lg-6">
                                <div class="product-pic-zoom">
                                    <asp:Image class="product-big-img" ID="ProductPhoto" runat="server" alt="" />
                                    <div class="zoom-icon">
                                        <i class="fa fa-search-plus"></i>
                                    </div>
                                </div>
                                <div class="product-thumbs">
                                    <div class="product-thumbs-track ps-slider owl-carousel">
                                        <div class="pt active" runat="server" id="Image1Container" data-imgbigurl="">
                                            <asp:Image ID="Image1" runat="server" alt="" />
                                        </div>
                                        <div class="pt" runat="server" id="Image2Container" data-imgbigurl="">
                                            <asp:Image ID="Image2" runat="server" alt="" />
                                        </div>
                                        <div class="pt" runat="server" id="Image3Container" data-imgbigurl="">
                                            <asp:Image ID="Image3" runat="server" alt="" />
                                        </div>
                                        <div class="pt" runat="server" id="Image4Container" data-imgbigurl="">
                                            <asp:Image ID="Image4" runat="server" alt="" />
                                        </div>
                                        <div class="pt" runat="server" id="Image5Container" data-imgbigurl="">
                                            <asp:Image ID="Image5" runat="server" alt="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="product-details">
                                    <div class="pd-title">
                                        <asp:Label CssClass="productName" runat="server" ID="ProductHeader" style="display: inline;"></asp:Label>
                                        <asp:ImageButton runat="server" OnClick="ProductHeart_Click" ID="ProductHeart" ImageUrl="img/heart-empty.jpg" style="margin-left: 20px; display: inline;"/>
                                    </div>
                                    <div class="pd-rating">
                                        <asp:Image ID="RatingImg" runat="server" />
                                        <asp:Label style="display: inline;" ID="Rates" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="pd-desc">
                                        <asp:Label runat="server" ID="Description"></asp:Label>
                                        <hr />
                                        <asp:Label runat="server" ID="Stock" style="color: red;"></asp:Label>
                                        <br />
                                        <asp:Label runat="server" ID="Price" class="text-info mt-5" ></asp:Label>
                                    </div>
                                    <%--<div class="pd-color">
                                        <h6>Color</h6>
                                        <div class="pd-color-choose">
                                            <div class="cc-item">
                                                <input type="radio" id="cc-black">
                                                <label for="cc-black"></label>
                                            </div>
                                            <div class="cc-item">
                                                <input type="radio" id="cc-yellow">
                                                <label for="cc-yellow" class="cc-yellow"></label>
                                            </div>
                                            <div class="cc-item">
                                                <input type="radio" id="cc-violet">
                                                <label for="cc-violet" class="cc-violet"></label>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="quantity">
                                        <div class="pro-qty">
                                            <input id="Quantity" type="text" value="1">
                                        </div>
                                        <button type="button" id="addCartBtn" class="primary-btn pd-cart">Add To Cart</button>
                                    </div>
                                    <p id="QuantityError" class="text-danger"></p>
                                    <ul class="pd-tags">
                                        <li><span>CATEGORIES</span>: More <a href="" runat="server" id="Category"></a></li>
                                        <li runat="server" id="Tags"><span>TAGS</span>: </a>, <a href="" runat="server" id="TagsBrand"></a></li>
                                    </ul>
                                    <p class="text-info mt-5">ID: #<asp:Label runat="server" class="productID" ID="Product_id"></asp:Label></p>
                                    <div class="pd-share">

                                    </div>
                                </div>
                            </div>
                    </div>
                    <div class="product-tab">
                        <div class="tab-item">
                            <ul class="nav" role="tablist">
                                <%--<li>
                                    <a data-toggle="tab" href="#tab-1" role="tab">DESCRIPTION</a>
                                </li>--%>
                                <li>
                                    <a data-toggle="tab" href="#tab-2" role="tab">SPECIFICATIONS</a>
                                </li>
                                <li>
                                    <asp:LinkButton ID="CommentBtn" class="active" data-toggle="tab" href="#tab-3" role="tab" runat="server">COMMENTS</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                        <div class="tab-item-content">
                            <div class="tab-content">
                                <%--<div class="tab-pane fade" id="tab-1" role="tabpanel">
                                    <div class="product-content">
                                        <div class="row">
                                            <div class="col-lg-7">
                                                <h5>Introduction</h5>
                                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
                                                    eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
                                                    ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                                                    aliquip ex ea commodo consequat. Duis aute irure dolor in </p>
                                                <h5>Features</h5>
                                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
                                                    eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
                                                    ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                                                    aliquip ex ea commodo consequat. Duis aute irure dolor in </p>
                                            </div>
                                            <div class="col-lg-5">
                                                <img src="img/product-single/tab-desc.jpg" alt="">
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="tab-pane fade" id="tab-2" role="tabpanel">
                                    <div class="specification-table" runat="server" id="Smartphone">
                                        <table>
                                            <tr>
                                                <td class="p-category">Price</td>
                                                <td>
                                                    <h5 runat="server" id="PriceSmartphone" class="p-price product-price"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Memory</td>
                                                <td>
                                                    <h5 runat="server" id="MemorySmartphone" class="p-memory"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Screen</td>
                                                <td>
                                                    <h5 runat="server" id="ScreenSmartphone" class="p-screen"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Battery</td>
                                                <td>
                                                    <h5 runat="server" id="BatterySmartphone" class="p-battery"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Processor</td>
                                                <td>
                                                    <h5 runat="server" id="ProcessorSmartphone" class="p-processor"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Caamera</td>
                                                <td>
                                                    <h5 runat="server" id="CameraSmartphone" class="p-camera"></h5>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="specification-table" runat="server" id="Gaming">
                                        <table>
                                            <tr>
                                                <td class="p-category">Price</td>
                                                <td>
                                                    <h5 runat="server" class="p-price product-price"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Memory</td>
                                                <td>
                                                    <h5 runat="server" class="p-memory"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Screen</td>
                                                <td>
                                                    <h5 runat="server" class="p-screen"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Battery</td>
                                                <td>
                                                    <h5 runat="server" class="p-battery"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Processor</td>
                                                <td>
                                                    <h5 runat="server" class="p-processor"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Caamera</td>
                                                <td>
                                                    <h5 runat="server" class="p-camera"></h5>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="specification-table" runat="server" id="Accessories">
                                        <table>
                                            <tr>
                                                <td class="p-category">Price</td>
                                                <td>
                                                    <h5 runat="server" class="p-price product-price"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Memory</td>
                                                <td>
                                                    <h5 runat="server" class="p-memory"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Screen</td>
                                                <td>
                                                    <h5 runat="server" class="p-screen"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Battery</td>
                                                <td>
                                                    <h5 runat="server" class="p-battery"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Processor</td>
                                                <td>
                                                    <h5 runat="server" class="p-processor"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Caamera</td>
                                                <td>
                                                    <h5 runat="server" class="p-camera"></h5>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="specification-table" runat="server" id="Headphones">
                                        <table>
                                            <tr>
                                                <td class="p-category">Price</td>
                                                <td>
                                                    <h5 runat="server" class="p-price product-price"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Memory</td>
                                                <td>
                                                    <h5 runat="server" class="p-memory"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Screen</td>
                                                <td>
                                                    <h5 runat="server" class="p-screen"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Battery</td>
                                                <td>
                                                    <h5 runat="server" class="p-battery"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Processor</td>
                                                <td>
                                                    <h5 runat="server" class="p-processor"></h5>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-category">Caamera</td>
                                                <td>
                                                    <h5 runat="server" class="p-camera"></h5>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane fade-in active" id="tab-3" role="tabpanel">
                                    <div class="customer-review-option">
                                        <h4 id="CommentHeader" runat="server"></h4>
                                        <div class="comment-option" runat="server">
                                            <asp:Repeater ID="CommentsRepeater" runat="server">
                                                <HeaderTemplate></HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="co-item" data-id="<%# Eval("id") %>" href="<%# Eval("id") %>">
                                                        <div class="avatar-text">
                                                            <h5><%# Eval("Commentor") %></h5>
                                                            <div class="at-reply">
                                                                <p class="comment"><%# Eval("Comment") %></p>
                                                            </div>
                                                            <span class="like-value"><%# Eval("Likes")  %></span>
                                                            <img class="CommentLikeBtn" src="img/thumbs-up.png" data-liked="<%# Eval("Liked") %>" style="width: 5%; height: auto;"/>
                                                            <span class="dislike-value"><%# Eval("Dislikes") %></span>
                                                            <img class="CommentDislikeBtn" src="img/thumbs-down.png" data-disliked="<%# Eval("Disliked") %>" style="width: 5%; height: auto;"/>
                                                            <br />
                                                            <hr />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="leave-comment">
                                            <h4>Leave A Comment</h4>
                                            <div class="row form-group">
                                                <div class="col-lg-12 mt-2">
                                                    <textarea placeholder="Message" id="CommentArea" class="form-control" style="height: 200px;"></textarea>
                                                    <button type="button" id="AddComment" class="site-btn">Send message</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </section>
    <!-- Product Shop Section End -->

    <!-- Related Products Section End -->
    <%--<div class="related-products spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title">
                        <h2>Related Products</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-sm-6">
                    <div class="product-item">
                        <div class="pi-pic">
                            <img src="img/products/women-1.jpg" alt="">
                            <div class="sale">Sale</div>
                            <div class="icon">
                                <i class="icon_heart_alt"></i>
                            </div>
                            <ul>
                                <li class="w-icon active"><a href="#"><i class="icon_bag_alt"></i></a></li>
                                <li class="quick-view"><a href="#">+ Quick View</a></li>
                                <li class="w-icon"><a href="#"><i class="fa fa-random"></i></a></li>
                            </ul>
                        </div>
                        <div class="pi-text">
                            <div class="catagory-name">Coat</div>
                            <a href="#">
                                <h5>Pure Pineapple</h5>
                            </a>
                            <div class="product-price">
                                $14.00
                                <span>$35.00</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <div class="product-item">
                        <div class="pi-pic">
                            <img src="img/products/women-2.jpg" alt="">
                            <div class="icon">
                                <i class="icon_heart_alt"></i>
                            </div>
                            <ul>
                                <li class="w-icon active"><a href="#"><i class="icon_bag_alt"></i></a></li>
                                <li class="quick-view"><a href="#">+ Quick View</a></li>
                                <li class="w-icon"><a href="#"><i class="fa fa-random"></i></a></li>
                            </ul>
                        </div>
                        <div class="pi-text">
                            <div class="catagory-name">Shoes</div>
                            <a href="#">
                                <h5>Guangzhou sweater</h5>
                            </a>
                            <div class="product-price">
                                $13.00
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <div class="product-item">
                        <div class="pi-pic">
                            <img src="img/products/women-3.jpg" alt="">
                            <div class="icon">
                                <i class="icon_heart_alt"></i>
                            </div>
                            <ul>
                                <li class="w-icon active"><a href="#"><i class="icon_bag_alt"></i></a></li>
                                <li class="quick-view"><a href="#">+ Quick View</a></li>
                                <li class="w-icon"><a href="#"><i class="fa fa-random"></i></a></li>
                            </ul>
                        </div>
                        <div class="pi-text">
                            <div class="catagory-name">Towel</div>
                            <a href="#">
                                <h5>Pure Pineapple</h5>
                            </a>
                            <div class="product-price">
                                $34.00
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <div class="product-item">
                        <div class="pi-pic">
                            <img src="" alt="">
                            <div class="icon">
                                <i class="icon_heart_alt"></i>
                            </div>
                            <ul>
                                <li class="w-icon active"><a href="#"><i class="icon_bag_alt"></i></a></li>
                                <li class="quick-view"><a href="#">+ Quick View</a></li>
                                <li class="w-icon"><a href="#"><i class="fa fa-random"></i></a></li>
                            </ul>
                        </div>
                        <div class="pi-text">
                            <div class="catagory-name">Towel</div>
                            <a href="#">
                                <h5>Converse Shoes</h5>
                            </a>
                            <div class="product-price">
                                $34.00
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <!-- Related Products Section End -->
</asp:Content>
