  <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="E_commerce.WebForm16" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Shopping Cart Section Begin -->
    <section class="shopping-cart spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <h2>ORDERS</h2>
                    <div class="cart-table">
                        <table style="width: 100%;" class="ProductsTable">
                            <thead>
                                <tr>
                                    <th>Image</th>
                                    <th class="p-name">Product Name</th>
                                    <th>Cost</th>
                                    <th>Status</th>
                                    <th>Date</th>
                                    <th>Rate/Comment</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="OrdersRepeater" runat="server">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Rating") %>" id="<%#Eval("id") %>">
                                            <td class="cart-pic first-row"><img src=<%#Eval("ProductImage") %> alt=""></td>
                                            <td class="cart-title first-row">
                                                <a href="Product?Name=<%#Eval("ProductName") %>"><h5><%#Eval("ProductName") %> x <%#Eval("Quantity") %></h5></a>
                                            </td> 
                                            <td class="p-price first-row">$ <%#Eval("TotalPrice") %></td>
                                            <td>
                                                <h5 class="text-center"><%#Eval("OrderStatus") %></h5>
                                            </td>
                                            <td>
                                                <h5 class="text-center"><%#Eval("OrderDateTime") %></h5>
                                            </td>
                                            <td>
                                                <div class="pd-rating" name='<%#Eval("Product_id") %>' runat="server">
                                                    <fieldset class="rate" runat="server">
                                                        <input type="radio" name="rating" value="10" /><label class="star" name="10" for="rating10" title="5 stars"></label>
                                                        <input type="radio" name="rating" value="9" /><label class="half star" name="9" for="rating9" title="4.5 stars"></label>
                                                        <input type="radio" name="rating" value="8" /><label class="star" name="8" for="rating8" title="4 stars"></label>
                                                        <input type="radio" name="rating" value="7" /><label class="half star" name="7" for="rating7" title="3.5 stars"></label>
                                                        <input type="radio" name="rating" value="6" /><label class="star" name="6" for="rating6" title="3 stars"></label>
                                                        <input type="radio" name="rating" value="5" /><label class="half star" name="5" for="rating5" title="2.5 stars"></label>
                                                        <input type="radio" name="rating" value="4" /><label class="star" name="4" for="rating4" title="2 stars"></label>
                                                        <input type="radio" name="rating" value="3" /><label class="half star" name="3" for="rating3" title="1.5 stars"></label>
                                                        <input type="radio" name="rating" value="2" /><label class="star" name="2" for="rating2" title="1 star"></label>
                                                        <input type="radio" name="rating" value="1" /><label class="half star" name="1" for="rating1" title="0.5 star"></label>
                                                    </fieldset>
                                                </div>
                                                <img class="RatingImg" src="" alt="RatedStars"/>
                                                <br />
                                                <a href="Product?Name=<%#Eval("ProductName") %>" style="color: orange;">Write a comment</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
    <script src="js/Orders.js"></script>
    <!-- Shopping Cart Section End -->
</asp:Content>
