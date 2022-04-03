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
    public partial class WebForm14 : System.Web.UI.Page
    {
        Products newProduct = new Products();
        List<Products> products = new List<Products>();
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
            string SearchValue;

            SearchValue = Request.QueryString["Search"];
            tempTR = newProduct.SearchProduct(SearchValue);

            ProductsRepeater.DataSource = tempTR.filteredProducts;
            ProductsRepeater.DataBind();
        }
    }
}