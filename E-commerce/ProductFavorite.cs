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
    public class ProductFavorite
    {
        public int idProductFavorite { get; set; }
        public int User_id { get; set; }
        public string User_Mail { get; set; }
        public int Product_id { get; set; }
        

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult GetProductFavorites(string User_Mail)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.favoriteproducts WHERE User_Mail = @User_Mail;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_Mail", User_Mail);

                    adapter.Fill(tr.favoriteProducts, "favoriteproducts");
                }
                return tr;
            }
            catch (Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }

        public TResult AddFavoriteProduct(int User_id, int Product_id)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.favoriteproducts(User_id, Product_id) VALUES (@User_id, @Product_id);"
                        , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                return tr;
            }
            catch (Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }

        public TResult RemoveFavoriteProduct(int User_id, int Product_id)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "DELETE FROM u7408250_onder.favoriteproducts WHERE User_id = @User_id AND Product_id = @Product_id;"
                        , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                return tr;
            }
            catch (Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }
    }
}