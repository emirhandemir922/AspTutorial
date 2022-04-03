using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspTutorial;
using System.Data;

namespace E_commerce
{
    public partial class SiteMaster : MasterPage
    {
        WebService1 service = new WebService1();
        User user = new User();
        CartItems cartProduct = new CartItems();
        Products product = new Products();
        ProductFavorite productfavorite = new ProductFavorite();
        List<Products> products = new List<Products>();
        List<ProductFavorite> productFavorites = new List<ProductFavorite>();
        TResult tempTR = new TResult();
        TResult mainTR = new TResult();

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
            tempProduct.Memory = int.Parse(row["Memory"].ToString());
            tempProduct.Screen = double.Parse(row["Screen"].ToString());
            tempProduct.Battery = int.Parse(row["Battery"].ToString());
            tempProduct.Processor = double.Parse(row["Processor"].ToString());
            tempProduct.Camera = int.Parse(row["Camera"].ToString());
            tempProduct.Favorite = false;
            tempProduct.Stock = int.Parse(row["Stock"].ToString());

            return tempProduct;
        }

        private CartItems MakeCartProducts(DataRow row)
        {
            CartItems tempProduct = new CartItems();

            tempProduct.id = int.Parse(row["id"].ToString());
            tempProduct.Product_id = int.Parse(row["Product_id"].ToString());
            tempProduct.ProductName = row["ProductName"].ToString();
            tempProduct.Price = int.Parse(row["Price"].ToString());
            tempProduct.User_id = int.Parse(row["User_id"].ToString());
            tempProduct.Quantity = int.Parse(row["Quantity"].ToString());

            return tempProduct;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string ProductName;
            DataRow datarow;

            user = service.GetSessionData();

            if (user == null)
            {
                int ProductsCount;
                int ProductQuantity;
                int ProductsQuantity;
                UserInfo.Text = "-Login-";
                OrdersBtn.Visible = false;
                LogOff.Visible = false;

                HttpCookie cartInfo = null;
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains("CartInfo"))
                {
                    cartInfo = HttpContext.Current.Request.Cookies["CartInfo"];
                    ProductsCount = int.Parse(cartInfo["ProductsCount"]);
                    ProductsQuantity = int.Parse(cartInfo["TotalQuantity"]);

                    for (int i = 1; i <= ProductsCount; i++)
                    {
                        if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                        {
                            HttpCookie cookieProduct = HttpContext.Current.Request.Cookies["Product" + i];
                            ProductName = cookieProduct["ProductName" + i];
                            ProductQuantity = int.Parse(cookieProduct["Quantity"]);

                            tempTR = product.SelectByName(ProductName);
                            datarow = tempTR.productsTable.Tables[0].Rows[0];
                            product = MakeProducts(datarow);

                            if (ProductQuantity > product.Stock) 
                            {
                                cartInfo["TotalQuantity"] = (ProductsQuantity - (ProductQuantity - product.Stock)).ToString();

                                ProductQuantity = product.Stock;
                                cookieProduct["Quantity"] = ProductQuantity.ToString();
                                cookieProduct["Message"] = "Low Stock!";

                                HttpContext.Current.Response.Cookies.Add(cartInfo);
                                HttpContext.Current.Response.Cookies.Add(cookieProduct);
                            }
                        }
                    }
                }
            }
            else
            {
                UserInfo.Text = user.Name + user.SurName;
                OrdersBtn.Visible = true;

                mainTR = cartProduct.GetShoppingCart(user.id);

                for (int i = 0; i < mainTR.shoppingTable.Tables[0].Rows.Count; i++) 
                {
                    datarow = mainTR.shoppingTable.Tables[0].Rows[i];
                    cartProduct = MakeCartProducts(datarow);

                    tempTR = product.SelectByName(cartProduct.ProductName);
                    datarow = tempTR.productsTable.Tables[0].Rows[0];
                    product = MakeProducts(datarow);

                    if (cartProduct.Quantity > product.Stock) 
                    {
                        cartProduct.Quantity = product.Stock;
                        cartProduct.UpdateShoppingCart(cartProduct);
                    }
                }
            }
        }

        protected void LogOff_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Request.RawUrl);
        }

        protected void UserInfo_Click(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("User.aspx");
            }
        }

        protected void Orders_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx");
        }
    }
}