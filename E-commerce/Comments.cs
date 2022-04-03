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
    public class Comments
    {
        public int id { get; set; }
        public string Comment { get; set; }
        public string Commentor { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Product_id { get; set; }
        public int User_id { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult ShowComments(int Product_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.comments WHERE Product_id = @Product_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);
                    adapter.Fill(tr.commentsTable, "comments");
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

        public TResult SelectComment(int Product_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT* FROM u7408250_onder.comments WHERE Product_id = @Product_id ORDER BY Likes DESC; ";
                    
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);
                    adapter.Fill(tr.commentsTable, "comments");
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

        public TResult AddComment(string Comment, string Commentor, int Likes, int Dislikes, int Product_id, int User_id)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.comments(Comment, Commentor, Likes, Dislikes, Product_id, User_id) VALUES (@Comment, @Commentor, @Likes, @Dislikes, @Product_id, @User_id);"
                    , connection);

                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Comment", Comment);
                    command.Parameters.AddWithValue("@Commentor", Commentor);
                    command.Parameters.AddWithValue("@Likes", Likes);
                    command.Parameters.AddWithValue("@Dislikes", Dislikes);
                    command.Parameters.AddWithValue("@Product_id", Product_id);
                    command.Parameters.AddWithValue("@User_id", User_id);
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

        public TResult CheckComment(int User_id, int Product_id) 
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.comments WHERE User_id = @User_id AND Product_id = @Product_id;";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);
                    adapter.SelectCommand.Parameters.AddWithValue("@Product_id", Product_id);
                    adapter.Fill(tr.commentsTable, "comments");

                    if (tr.commentsTable.Tables["comments"].Rows.Count > 0)
                    {
                        tr.isError = true;
                    }
                    else 
                    {
                        tr.isError = false;
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

        public TResult IncreaseLike(int id) 
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "UPDATE u7408250_onder.comments SET Likes = Likes + 1 WHERE id = @id; "
                    , connection);

                    command.Connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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

        public TResult IncreaseDislike(int id)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "UPDATE u7408250_onder.comments SET Dislikes = Dislikes + 1 WHERE id = @id; "
                    , connection);

                    command.Connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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

        public TResult DecreaseLike(int id)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "UPDATE u7408250_onder.comments SET Likes = Likes - 1 WHERE id = @id; "
                    , connection);

                    command.Connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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

        public TResult DecreaseDislike(int id)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "UPDATE u7408250_onder.comments SET Dislikes = Dislikes - 1 WHERE id = @id; "
                    , connection);

                    command.Connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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