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
    public class CommentLikes
    {
        public int id { get; set; }
        public int Comment_id { get; set; }
        public int User_id { get; set; }
        public int Liked { get; set; }
        public int Disliked { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult LikeComment(int User_id, int Comment_id, int Comment_Liked, int Comment_Disliked)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand( 
                        "INSERT INTO u7408250_onder.commentlikes(User_id ,Comment_id, Liked, Disliked) VALUES (@User_id, @Comment_id, @Liked, @Disliked);"
                    , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Comment_id", Comment_id);
                    command.Parameters.AddWithValue("@Liked", Liked);
                    command.Parameters.AddWithValue("@Disliked", Disliked);
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

        public TResult DislikeComment(int User_id, int Comment_id, int Comment_Liked, int Comment_Disliked)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.commentlikes(User_id ,Comment_id, Liked, Disliked) VALUES (@User_id, @Comment_id, @Liked, @Disliked);"
                    , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Comment_id", Comment_id);
                    command.Parameters.AddWithValue("@Liked", Liked);
                    command.Parameters.AddWithValue("@Disliked", Disliked);
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

        public TResult GetCommentLikes(int User_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT* FROM u7408250_onder.commentlikes WHERE User_id = @User_id; ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);

                    adapter.Fill(tr.commentLikesTable, "commentlikes");
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

        public TResult SelectCommentLike(int User_id, int Comment_id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT* FROM u7408250_onder.commentlikes WHERE User_id = @User_id AND Comment_id = @Comment_id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@User_id", User_id);
                    adapter.SelectCommand.Parameters.AddWithValue("@Comment_id", Comment_id);

                    adapter.Fill(tr.commentLikesTable, "commentlikes");
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

        public TResult DeleteCommentLike(int User_id, int Comment_id) 
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "DELETE FROM u7408250_onder.commentlikes WHERE User_id = @User_id AND Comment_id = @Comment_id;"
                    , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@User_id", User_id);
                    command.Parameters.AddWithValue("@Comment_id", Comment_id);
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