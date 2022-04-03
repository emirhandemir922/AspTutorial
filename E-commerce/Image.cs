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
    public class Image
    {
        public int id { get; set; }
        public string PhotoName { get; set; }
        public string PhotoSource { get; set; }
        public int PhotoSize { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public bool CoverPhoto { get; set; }
        public int PhotoOrder { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult ShowImages(int Product_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.photos WHERE Product_id = @Product_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);
                    adapter.Fill(tr.imageTable, "photos");
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

        public TResult AddImage(string PhotoName, string PhotoSource, int PhotoSize, int Product_id, string ProductName, bool CoverPhoto)
        {
            try
            {
                using (connection)
                {
                    SelectImages(ProductName);

                    if (tr.imageTable.Tables[0].Rows.Count >= 1)
                    {
                        tr.isError = true;
                        tr.message = "Image already exists";

                    }
                    else
                    {
                        MySqlCommand command = new MySqlCommand(
                            "INSERT INTO u7408250_onder.photos(PhotoName, PhotoSource, PhotoSize, Product_id, ProductName, CoverPhoto) VALUES (@PhotoName, @PhotoSource, @PhotoSize, @Product_id, @ProductName, @CoverPhoto);"
                        , connection);

                        command.Connection.Open();
                        command.Parameters.AddWithValue("@PhotoName", PhotoName);
                        command.Parameters.AddWithValue("@PhotoSource", PhotoSource);
                        command.Parameters.AddWithValue("@PhotoSize", PhotoSize);
                        command.Parameters.AddWithValue("@Product_id", Product_id);
                        command.Parameters.AddWithValue("@ProductName", ProductName);
                        command.Parameters.AddWithValue("@CoverPhoto", CoverPhoto);
                        command.ExecuteNonQuery();
                        connection.Close();
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

        public TResult SelectImages(string ProductName)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT* FROM u7408250_onder.photos WHERE ProductName = @ProductName;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProductName", ProductName);
                    adapter.Fill(tr.imageTable, "photos");
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

        public TResult SelectCoverPhoto(string ProductName)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT* FROM u7408250_onder.photos WHERE ProductName = @ProductName AND CoverPhoto = 1;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProductName", ProductName);
                    adapter.Fill(tr.imageTable, "photos");
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
