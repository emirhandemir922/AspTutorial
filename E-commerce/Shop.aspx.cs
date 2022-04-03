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
    public partial class WebForm1 : System.Web.UI.Page
    {
        WebService1 service = new WebService1();
        Products product = new Products();
        User user = new User();
        Filters filters = new Filters();
        ProductFavorite productfavorite = new ProductFavorite();
        TResult tempTR = new TResult();
        TResult mainTR = new TResult();
        List<Products> products = new List<Products>();
        List<ProductFavorite> productFavorites = new List<ProductFavorite>();
        string ProductCategory;
        string SelectedSorting;
        int Page;

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
            tempProduct.SponsorRate = int.Parse(row["SponsorRate"].ToString());

            return tempProduct;
        }

        private ProductFavorite MakeProductFavorites(DataRow row)
        {
            ProductFavorite tempProductFavorite = new ProductFavorite();

            tempProductFavorite.idProductFavorite = int.Parse(row["id"].ToString());
            tempProductFavorite.User_id = int.Parse(row["User_id"].ToString());
            tempProductFavorite.Product_id = int.Parse(row["Product_id"].ToString());

            return tempProductFavorite;
        }

        [WebMethod]
        protected void Page_Load(object sender, EventArgs e)
        {
            user = service.GetSessionData();

            ProductCategory = Request.QueryString["category"];
            SelectedSorting = Request.QueryString["sort"];
            Page = int.Parse(Request.QueryString["page"]);

            if (Request.QueryString["brand"] == null)
            {
                filters.Brand = "Main";
            }
            else 
            {
                filters.Brand = Request.QueryString["brand"];
            }

            if (Request.QueryString["minPrice"] == null)
            {
                filters.MinPrice = 0;
            }
            else 
            {
                filters.MinPrice = int.Parse(Request.QueryString["minPrice"]);
            }

            if (Request.QueryString["maxPrice"] == null)
            {
                filters.MaxPrice = 0;
            }
            else
            {
                filters.MaxPrice = int.Parse(Request.QueryString["maxPrice"]);
            }

            if (Request.QueryString["minScreen"] == null)
            {
                filters.MinScreen = 0;
            }
            else
            {
                filters.MinScreen = double.Parse(Request.QueryString["minScreen"].Replace('.', ','));
            }

            if (Request.QueryString["maxScreen"] == null)
            {
                filters.MaxScreen = 0;
            }
            else
            {
                filters.MaxScreen = double.Parse(Request.QueryString["maxScreen"].Replace('.', ','));
            }

            if (Request.QueryString["memory"] == null || Request.QueryString["memory"] == "")
            {
                filters.Memory = 99;
            }
            else
            {
                filters.EmptyString = Request.QueryString["memory"].ToString();
            }

            tempTR = product.GetProducts(filters, SelectedSorting, ProductCategory, Page);
            ProductsCount.Text = tempTR.outputTable.Tables[0].Rows.Count.ToString();

            if (tempTR.productsTable.Tables.Count > 0)
            {
                foreach (DataRow datarow in tempTR.productsTable.Tables[0].Rows)
                {
                    products.Add(MakeProducts(datarow));
                }
            }

            if (user != null)
            {
                tempTR = productfavorite.GetProductFavorites(user.Email);

                foreach (DataRow datarow in tempTR.favoriteProducts.Tables[0].Rows)
                {
                    productFavorites.Add(MakeProductFavorites(datarow));
                }

                foreach (Products memberProduct in products)
                {
                    foreach (ProductFavorite memberProductFavorite in productFavorites)
                    {
                        if (memberProduct.id == memberProductFavorite.Product_id && memberProductFavorite.User_id == user.id)
                        {
                            memberProduct.Favorite = true;
                        }
                    }
                }
            }

            foreach (Products memberProduct in products) 
            {
                if (memberProduct.SponsorRate > 0)
                {
                    memberProduct.Sponsor = "Ad";
                }
            }

            ProductsRepeater.DataSource = products;
            ProductsRepeater.DataBind();
        }
    }
}