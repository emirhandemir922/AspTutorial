using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspTutorial;
using System.Data;

namespace E_commerce
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        User newUser = new User();
        TResult tempTR = new TResult();

        public User MakeUser(DataRow row) 
        {
            newUser.id = int.Parse(row["id"].ToString());
            newUser.Name = row["Name"].ToString();
            newUser.SurName = row["SurName"].ToString();
            newUser.Email = row["Email"].ToString();
            newUser.Type = row["Type"].ToString();

            return newUser;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null)
            {
                User_id.InnerText = Session["idUser"].ToString();

                tempTR = newUser.SelectUserByID(int.Parse(Session["idUser"].ToString()));
                newUser = MakeUser(tempTR.user.Tables[0].Rows[0]);
            }
            else
            {
                Header.Text = "You need to login to view your profile information.";
            }

            Name.Value = newUser.Name;
            SurName.Value = newUser.SurName;
            Email.Value = newUser.Email;
            Password.Value = "**********";
        }

        protected void AddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProduct.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            newUser.Name = Name.Value;
            newUser.SurName = SurName.Value;
            newUser.Email = Email.Value;

            if (Password.Value != "**********") {
                newUser.Password = Password.Value;
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUser, Newtonsoft.Json.Formatting.Indented);

            ProductService.WebService1SoapClient service = new ProductService.WebService1SoapClient();
            service.EditUser(json);
        }
    }
}