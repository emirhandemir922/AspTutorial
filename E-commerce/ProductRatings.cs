using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AspTutorial
{
    public class ProductRatings
    {
        public int id { get; set; }
        public int User_id { get; set; }
        public int Product_id { get; set; }
        public int Order_id { get; set; }
        public int Rating { get; set; }
        public DateTime RatingDateTime { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult GetProductRatingsWProductID(int Product_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.productratings WHERE Product_id = @Product_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);

                    adapter.Fill(tr.productRatingsTable, "productratings");
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

        public TResult GetProductRatingsWUserID(int User_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.productratings WHERE User_id = @User_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);

                    adapter.Fill(tr.productRatingsTable, "productratings");
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

        public TResult RateProduct(int User_id, int Product_id, int Order_id, int RatingValue, DateTime RatingDateTime)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.productratings(User_id, Product_id, Order_id, Rating, RatingDateTime) VALUES (@User_id, @Product_id, @Order_id, @RatingValue, @RatingDateTime);"
                        , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.Parameters.AddWithValue("@Order_id", Order_id);
                    command.Parameters.AddWithValue("@RatingValue", RatingValue);
                    command.Parameters.AddWithValue("@RatingDateTime", RatingDateTime);
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

        public TResult DeleteProductRating(int User_id, int Product_id) 
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.productratings(User_id, Product_id) VALUES (@User_id, @Product_id);"
                        , connection);
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.ExecuteNonQuery();
                    connection.Close();
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