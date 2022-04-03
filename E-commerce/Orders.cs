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
    public class Orders
    {
        public int id { get; set; }
        public int Product_id { get; set; }
        public string ProductName { get; set; }
        public string ProductImage{ get; set; }
        public int Seller_id { get; set; }
        public int User_id { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public int Price { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int Rating { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult AddOrder(int Product_id, string ProductName, string ProductImage, int Quantity, int TotalPrice, int User_id, int Seller_id, string OrderStatus, DateTime OrderDateTime)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.orders(Product_id, ProductName, ProductImage, Quantity, TotalPrice, User_id, Seller_id, OrderStatus, OrderDateTime) VALUES (@Product_id, @ProductName, @ProductImage, @Quantity, @TotalPrice, @User_id, @Seller_id, @OrderStatus, @OrderDateTime);"
                    , connection);

                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    command.Parameters.AddWithValue("@ProductImage", ProductImage);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Seller_id", Seller_id);
                    command.Parameters.AddWithValue("@OrderStatus", OrderStatus);
                    command.Parameters.AddWithValue("@OrderDateTime", OrderDateTime);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                tr.isError = false;
                return tr;
            }
            catch (Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }

        public TResult CheckOrder(int User_id, int Product_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.orders WHERE User_id = @User_id AND Product_id = @Product_id;";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);
                    adapter.Fill(tr.ordersTable, "orders");

                    if (tr.ordersTable.Tables["orders"].Rows.Count > 0)
                    {
                        if (tr.ordersTable.Tables["orders"].Rows[0][8].ToString() == "Delivered")
                        {
                            tr.isError = false;
                        }
                        else
                        {
                            tr.isError = true;
                        }
                    }
                    else
                    {
                        tr.isError = true;
                    }
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

        public TResult GetOrders(int User_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.orders WHERE User_id = @User_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);
                    adapter.Fill(tr.ordersTable, "orders");
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