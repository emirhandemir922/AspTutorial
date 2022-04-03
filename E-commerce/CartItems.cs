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
    public class CartItems
    {
        public int id { get; set; }
        public int Product_id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int User_id { get; set; }
        public int Quantity { get; set; }
        public string Photo { get; set; }
        public int Seller_id { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult AddShoppingCart(int Product_id, string ProductName, double Price, int User_id, int Quantity, string Photo, int Seller_id)
        {
            try
            {
                using (connection)
                {
                    command = new MySqlCommand("INSERT INTO shoppingcart(Product_id, ProductName, Price, User_id, Quantity, Photo, Seller_id) VALUES(@Product_id, @ProductName, @Price, @User_id, @Quantity, @Photo, @Seller_id);", connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@Photo", Photo);
                    command.Parameters.AddWithValue("@Seller_id", Seller_id);
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
        public TResult IncreaseQuantity(int Product_id, int User_id, int Quantity)
        {
            try
            {
                using (connection)
                {
                    command = new MySqlCommand("UPDATE u7408250_onder.shoppingcart SET Quantity = Quantity + @Quantity WHERE Product_id = @Product_id AND User_id = @User_id;", connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.Parameters.AddWithValue("@User_id", User_id);
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
        public TResult RemoveFromShoppingCart(int Product_id, int User_id)
        {
            try
            {
                using (connection)
                {
                    command = new MySqlCommand("UPDATE u7408250_onder.shoppingcart SET Quantity = Quantity - 1 WHERE User_id = @User_id AND Product_id = @Product_id;", connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.Parameters.AddWithValue("@User_id", User_id);
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
        public TResult UpdateShoppingCart(CartItems cartItem)
        {
            try
            {
                using (connection)
                {
                    command = new MySqlCommand("UPDATE u7408250_onder.shoppingcart SET Product_id = @Product_id, ProductName = @ProductName, Price = @Price, User_id = @User_id, Quantity = @Quantity WHERE id = @ID;", connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@ID", cartItem.id);
                    command.Parameters.AddWithValue("@Product_id", cartItem.Product_id);
                    command.Parameters.AddWithValue("@ProductName", cartItem.ProductName);
                    command.Parameters.AddWithValue("@Price", cartItem.Price);
                    command.Parameters.AddWithValue("@User_id", cartItem.User_id);
                    command.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
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
        public TResult GetShoppingCart(int User_id)
        {
            tr.shoppingTable.Clear();

            try
            {
                using (connection)
                {
                    String command = "SELECT *FROM shoppingCart WHERE User_id = @User_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);
                    adapter.Fill(tr.shoppingTable, "products");
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
        public TResult CheckShoppingCart(int User_id, int Product_id)
        {
            try
            {
                using (connection)
                {
                    String command = "SELECT *FROM shoppingcart WHERE User_id = @User_id AND Product_id = @Product_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);
                    adapter.Fill(tr.shoppingTable, "products");
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
        public TResult RemoveShoppingCart(int User_id)
        {
            try
            {
                using (connection)
                {
                    command = new MySqlCommand("DELETE FROM u7408250_onder.shoppingcart WHERE User_id=@User_id;", connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
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