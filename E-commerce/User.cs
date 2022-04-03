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
    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();

        public TResult Login(string Email, string Password)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.users WHERE Email = @Email AND Password = @Password;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Email", Email);
                    adapter.SelectCommand.Parameters.AddWithValue("@Password", Password);
                    adapter.Fill(tr.user, "users");

                    if (tr.user.Tables[0].Rows.Count == 1)
                    {
                        tr.isError = false;
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
        public TResult Register(string Name, string SurName, string Email, string Password, string Type)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "INSERT INTO u7408250_onder.users(Name, SurName, Email, Password, Type) VALUES(@Name, @SurName, @Email, @Password, @Type)"
                        , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@SurName", SurName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Type", Type);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                tr = SelectUserByMail(Email);
                return tr;
            }
            catch (Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }

        public TResult SelectUserByMail(string Email)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT *FROM u7408250_onder.users WHERE Email = @Email";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Email", Email);
                    adapter.Fill(tr.user, "users");
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
        public TResult SelectUserByID(int ID)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT *FROM u7408250_onder.users WHERE id = @ID";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ID", ID);
                    adapter.Fill(tr.user, "users");
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

        public TResult EditUser(int id, string Name, string SurName, string Email, string Password)
        {
            try
            {
                using (connection)
                {
                    if (Password == "Dummy")
                    {
                        MySqlCommand command = new MySqlCommand(
                            "UPDATE  u7408250_onder.users SET Name = @Name, SurName = @Surname, Email = @Email WHERE id = @User_id;"
                            , connection);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@SurName", SurName);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@User_id", id);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        MySqlCommand command = new MySqlCommand(
                            "UPDATE  u7408250_onder.users SET Name = @Name, SurName = @Surname, Email = @Email, Password = @Password WHERE id = @User_id;"
                            , connection);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@SurName", SurName);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@User_id", id);
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
    }
}