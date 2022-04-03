using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AspTutorial;
using E_commerce;

namespace E_commerce
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        User newUser = new User();
        TResult tempTR = new TResult();
        Products newProduct = new Products();
        CartItems newCartProduct = new CartItems();
        string ProductName;
        int ProductQuantity;
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

        protected void loginButton_Click(object sender, EventArgs e)
        {
            bool addNewProduct = true;
            mailError.Visible = false;
            passwordError.Visible = false;

            newUser.Email = loginMail.Value;
            newUser.Password = loginPassword.Value;

            tempTR = newUser.Login(newUser.Email, newUser.Password);

            if (!tempTR.isError)
            {
                tempTR = newUser.SelectUserByMail(newUser.Email);

                Session.Add("idUser", int.Parse(tempTR.user.Tables[0].Rows[0]["id"].ToString()));
                Session.Add("Name", tempTR.user.Tables[0].Rows[0]["Name"].ToString());
                Session.Add("SurName", tempTR.user.Tables[0].Rows[0]["SurName"].ToString());
                Session.Add("Email", tempTR.user.Tables[0].Rows[0]["Email"].ToString());

                HttpCookie cartInfo = null;
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains("CartInfo"))
                {
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

                            tempTR = newCartProduct.GetShoppingCart(int.Parse(Session["idUser"].ToString()));
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
                                tempTR = newCartProduct.IncreaseQuantity(newProduct.id, int.Parse(Session["idUser"].ToString()), ProductQuantity);
                            }
                            else
                            {
                                tempTR = newCartProduct.AddShoppingCart(newProduct.id, newProduct.ProductName, newProduct.Price, int.Parse(Session["idUser"].ToString()), ProductQuantity, newProduct.Photo, newProduct.Seller_id);
                            }
                        }
                    }

                    cartInfo.Expires = DateTime.Now.AddDays(-30d);
                    HttpContext.Current.Response.Cookies.Add(cartInfo);
                }

                Response.Cookies["relocation"].Value = "true";
                Response.Redirect(Request.Cookies["relocation-link"].Value.ToString());
            }
            else
            {
                Header.Text = "Something went wrong !";

                mailError.Visible = true;
                passwordError.Visible = true;
            }
        }
    }
}