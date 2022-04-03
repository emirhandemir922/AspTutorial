using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspTutorial;
using System.Data;
using System.Web.UI.HtmlControls;

namespace E_commerce
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        Products newProduct = new Products();
        Orders newOrder = new Orders();
        CartItems newCartItems = new CartItems();
        List<CartItems> shoppingCartItems = new List<CartItems>();
        TResult tempTR = new TResult();
        int PriceTotal = 0;

        private CartItems MakeShoppingCartProducts(DataRow row) 
        {
            CartItems tempCartItem = new CartItems();

            tempCartItem.Product_id = int.Parse(row["Product_id"].ToString());
            tempCartItem.ProductName = row["ProductName"].ToString();
            tempCartItem.Price = int.Parse(row["Price"].ToString());
            tempCartItem.User_id = int.Parse(row["User_id"].ToString());
            tempCartItem.Quantity = int.Parse(row["Quantity"].ToString());
            tempCartItem.Photo = row["Photo"].ToString();

            return tempCartItem;
        }
        private Products MakeProducts(DataRow row)
        {
            Products tempProduct = new Products();

            tempProduct.id = int.Parse(row["id"].ToString());
            tempProduct.ProductName = row["ProductName"].ToString();
            tempProduct.Category = row["Category"].ToString();
            tempProduct.Brand = row["Brand"].ToString();
            tempProduct.Price = double.Parse(row["Price"].ToString());
            tempProduct.Photo = row["Photo"].ToString();
            tempProduct.Description = row["Description"].ToString();
            tempProduct.Rating = double.Parse(row["Rating"].ToString());
            tempProduct.Rates = int.Parse(row["Rates"].ToString());
            tempProduct.Favorite = false;

            return tempProduct;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUser"] != null)
            {
                CartItems cartProduct = new CartItems();
                int User_id;
                User_id = int.Parse(Session["idUser"].ToString());
                tempTR = cartProduct.GetShoppingCart(User_id);

                foreach (DataRow datarow in tempTR.shoppingTable.Tables[0].Rows)
                {
                    if (int.Parse(datarow["Quantity"].ToString()) > 0)
                    {
                        shoppingCartItems.Add(MakeShoppingCartProducts(datarow));
                    }
                }
            }
            else 
            {
                string ProductName;
                int ProductQuantity;
                int ProductsCount;
                string ProductImage;
                HttpCookie cartInfo = null;
                cartInfo = HttpContext.Current.Request.Cookies["CartInfo"];
                ProductsCount = int.Parse(cartInfo["ProductsCount"]);

                if (ProductsCount > 0)
                {
                    for (int i = 0; i <= ProductsCount; i++)
                    {
                        if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                        {
                            CartItems cartProduct = new CartItems(); 
                            HttpCookie cookieProduct = HttpContext.Current.Request.Cookies["Product" + i];
                            ProductName = cookieProduct["ProductName" + i];
                            ProductQuantity = int.Parse(cookieProduct["Quantity"]);

                            tempTR = newProduct.SelectByName(ProductName);
                            DataRow datarow = tempTR.productsTable.Tables[0].Rows[0];
                            newProduct = MakeProducts(datarow);

                            cartProduct.ProductName = ProductName;
                            cartProduct.Quantity = ProductQuantity;
                            cartProduct.Price = Convert.ToInt32(newProduct.Price);
                            cartProduct.Photo = newProduct.Photo;

                            shoppingCartItems.Add(cartProduct);
                        }
                    }
                }
            }

            ShoppingCartRepeater.DataSource = shoppingCartItems;
            ShoppingCartRepeater.DataBind();

            if (shoppingCartItems.Count > 0)
            {
                foreach (CartItems memberCartItem in shoppingCartItems)
                {
                    PriceTotal += memberCartItem.Price * memberCartItem.Quantity;
                }
            }

            Total.InnerText = "$" + PriceTotal.ToString();
        }

        protected void OrderButton_Click(object sender, EventArgs e) 
        {
            if (Session["idUser"] != null)
            {
                DateTime datetime = DateTime.Now;
                int User_id;
                User_id = int.Parse(Session["idUser"].ToString());

                foreach (CartItems memberCartItem in shoppingCartItems)
                {
                    newProduct.DecreaseStock(memberCartItem.ProductName, memberCartItem.Quantity);
                    tempTR = newOrder.AddOrder(memberCartItem.Product_id, memberCartItem.ProductName, memberCartItem.Photo, memberCartItem.Quantity, memberCartItem.Price * memberCartItem.Quantity, User_id, memberCartItem.Seller_id, "Order getting prepared", datetime);

                    if (tempTR.isError) 
                    {
                        Response.Redirect("ErrorPage.aspx");
                    }
                }

                newCartItems.RemoveShoppingCart(User_id);

                Response.Redirect("HomePage.aspx");
            }
            else 
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}