using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;

namespace AspTutorial
{
    public class Filters
    {
        public int id { get; set; }
        public string Brand { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int Memory { get; set; }
        public double MinScreen { get; set; }
        public double MaxScreen { get; set; }
        public int Battery { get; set; }
        public double Processor { get; set; }
        public int Camera { get; set; }
        public string EmptyString { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();
        List<Products> products = new List<Products>();

        public Products MakeProduct(DataRow row)
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

            return tempProduct;
        }

        public List<Products> UseFilters(List<Products> List, Filters filter) 
        {
            bool Remove;

            for (int i = 0; i < List.Count; i++) 
            {
                Remove = false;

                if (List[i].Brand != filter.Brand && filter.Brand != null) 
                {
                    Remove = true;
                }

                if (List[i].Price < filter.MinPrice || List[i].Price > filter.MaxPrice)
                {
                    Remove = true;
                }

                if (List[i].Memory != filter.Memory && filter.Memory != 0)
                {
                    Remove = true;
                }

                if (List[i].Screen < filter.MinScreen || List[i].Screen > filter.MaxScreen)
                {
                    Remove = true;
                }

                //if (List[i].Battery != filter.Battery && filter.Battery != null)
                //{
                //    Remove = true;
                //}

                //if (List[i].Processor != filter.Processor && filter.Processor != null)
                //{
                //    Remove = true;
                //}

                //if (List[i].Camera != filter.Camera && filter.Camera != null)
                //{
                //    Remove = true;
                //}

                if (Remove) 
                {
                    List.RemoveAt(i);
                    i = i - 1;
                }
            }

            return List;
        }

        //public TResult FilterProducts(Filters filter) 
        //{
        //    try
        //    {
        //        using (connection)
        //        {
        //            string command = "SELECT* FROM u7408250_onder.products ORDER BY Rates DESC;";
        //            MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
        //            adapter.Fill(tr.productsTable, "products");

        //            foreach (DataRow member in tr.productsTable.Tables[0].Rows) 
        //            {
        //                products.Add(MakeProduct(member));
        //            }

        //            products = UseFilters(products, filter);
        //            tr.filteredProducts = products;
        //        }
        //        return tr;
        //    }
        //    catch (Exception ex)
        //    {
        //        tr.message = ex.ToString();
        //        tr.isError = true;
        //        return tr;
        //    }
        //}
    }
}