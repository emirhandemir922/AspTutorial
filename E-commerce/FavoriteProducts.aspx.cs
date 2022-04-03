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
    public partial class WebForm12 : System.Web.UI.Page
    {
        ProductService.WebService1SoapClient service = new ProductService.WebService1SoapClient();
        Products product = new Products();
        ProductFavorite productfavorite = new ProductFavorite();
        TResult tempTR = new TResult();
        TResult mainTR = new TResult();
        List<Products> products = new List<Products>();
        List<ProductFavorite> productFavorites = new List<ProductFavorite>();
        string User_Mail;

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

        private ProductFavorite MakeProductFavorites(DataRow row)
        {
            ProductFavorite tempProductFavorite = new ProductFavorite();

            tempProductFavorite.idProductFavorite = int.Parse(row["id"].ToString());
            tempProductFavorite.User_id = int.Parse(row["User_id"].ToString());
            tempProductFavorite.Product_id = int.Parse(row["Product_id"].ToString());

            return tempProductFavorite;
        }

        [WebMethod(EnableSession = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Mail = Session["Email"].ToString();

            tempTR = productfavorite.GetProductFavorites(User_Mail);
            foreach (DataRow datarow in tempTR.favoriteProducts.Tables[0].Rows)
            {
                productFavorites.Add(MakeProductFavorites(datarow));
            }

            for(int i = 0; i < productFavorites.Count; i++)
            {
                tempTR = product.SelectByID(productFavorites[i].Product_id);
                DataRow datarow = tempTR.productsTable.Tables[0].Rows[i];
                products.Add(MakeProducts(datarow));
            }

            FavoriteProductsRepeater.DataSource = products;
            FavoriteProductsRepeater.DataBind();
        }
    }
}