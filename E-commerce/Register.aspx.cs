using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using AspTutorial;

namespace E_commerce
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        WebService1 service = new WebService1();
        User newUser = new User();
        Products newProduct = new Products();
        CartItems newCartProduct = new CartItems();
        TResult tempTR = new TResult();

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
            if (Request.Cookies["relocation-link"] == null)
            {
                Response.Cookies["relocation-link"].Value = Request.UrlReferrer.ToString();
            }

        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline);

            if (registerName.Value == "" || registerSurName.Value == "")
            {
                tempTR.isError = true;
            }

            if (registerPassword.Value.Length < 8)
            {
                tempTR.isError = true;
            }

            if (!(regex.IsMatch(registerMail.Value)) || registerMail.Value == "")
            {
                tempTR.isError = true;
            }

            if (!tempTR.isError)
            {
                tempTR = newUser.SelectUserByMail(registerMail.Value);

                if (tempTR.user.Tables[0].Rows.Count > 0)
                {
                    Header.Text = "This email already exists";
                }
                else
                {
                    tempTR = newUser.Register(registerName.Value, registerSurName.Value, registerMail.Value, registerPassword.Value, "Guest");

                    Session.Add("Name", registerName.Value);
                    Session.Add("SurName", registerSurName.Value);
                    Session.Add("Email", registerMail.Value);
                    Session.Timeout = 2000;

                    newUser = service.GetSessionData();

                    HttpCookie cartInfo = null;
                    if (HttpContext.Current.Request.Cookies.AllKeys.Contains("CartInfo"))
                    {
                        string ProductName;
                        int ProductQuantity;
                        bool addNewProduct = true;

                        cartInfo = HttpContext.Current.Request.Cookies["CartInfo"];
                        int ProductsCount = int.Parse(cartInfo["ProductsCount"]);
                        int ProductsQuantity = int.Parse(cartInfo["TotalQuantity"]);

                        for (int i = 1; i <= ProductsCount; i++)
                        {
                            if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                            {
                                HttpCookie cookieProduct = HttpContext.Current.Request.Cookies["Product" + i];
                                ProductName = cookieProduct["ProductName" + i];
                                ProductQuantity = int.Parse(cookieProduct["Quantity"]);

                                cookieProduct.Expires = DateTime.Now.AddDays(-30d);
                                HttpContext.Current.Response.Cookies.Add(cookieProduct);

                                tempTR = newProduct.SelectByName(ProductName);
                                DataRow datarow = tempTR.productsTable.Tables[0].Rows[0];
                                newProduct = MakeProducts(datarow);

                                tempTR = newCartProduct.GetShoppingCart(newUser.id);
                                foreach (DataRow row in tempTR.shoppingTable.Tables[0].Rows)
                                {
                                    if (int.Parse(row["Quantity"].ToString()) > 0)
                                    {
                                        if (row["ProductName"].ToString() == newProduct.ProductName)
                                        {
                                            addNewProduct = false;
                                        }
                                    }
                                }

                                if (!addNewProduct)
                                {
                                    tempTR = newCartProduct.IncreaseQuantity(newProduct.id, newUser.id, ProductQuantity);
                                }
                                else
                                {
                                    tempTR = newCartProduct.AddShoppingCart(newProduct.id, newProduct.ProductName, newProduct.Price, newUser.id, ProductQuantity, newProduct.Photo, newProduct.Seller_id);
                                }
                            }
                        }

                        cartInfo.Expires = DateTime.Now.AddDays(-30d);
                        HttpContext.Current.Response.Cookies.Add(cartInfo);
                    }
                
                    Response.Redirect("Homepage.aspx");
                }
            }
            else
            {
                Header.Text = "Please fill out the form correctly";
            }
        }
    }
}