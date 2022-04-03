using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspTutorial;
using System.Data;
using System.Web.UI.HtmlControls;

namespace E_commerce
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        private Orders MakeOrders(DataRow row, int Rating)
        {
            Orders tempOrder = new Orders();

            tempOrder.id = int.Parse(row["id"].ToString());
            tempOrder.Product_id = int.Parse(row["Product_id"].ToString());
            tempOrder.ProductName = row["ProductName"].ToString();
            tempOrder.ProductImage = row["ProductImage"].ToString();
            tempOrder.Seller_id = int.Parse(row["Seller_id"].ToString());
            tempOrder.User_id = int.Parse(row["User_id"].ToString());
            tempOrder.Quantity = int.Parse(row["Quantity"].ToString());
            tempOrder.TotalPrice = double.Parse(row["TotalPrice"].ToString());
            tempOrder.Price = Convert.ToInt32(tempOrder.TotalPrice) / tempOrder.Quantity;
            tempOrder.OrderStatus = row["OrderStatus"].ToString();
            tempOrder.OrderDateTime = DateTime.Parse(row["OrderDateTime"].ToString());
            tempOrder.Rating = Rating;

            return tempOrder;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == "" || Session["Email"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Products product = new Products();
                Orders order = new Orders();
                ProductRatings productRates = new ProductRatings();
                List<Orders> orders = new List<Orders>();
                int User_id = int.Parse(Session["idUser"].ToString());
                bool booleanCheck;
                TResult tempTR = new TResult();
                TResult tempTR2 = new TResult();

                if (orders.Count > 0) 
                {
                    orders.Clear();
                }

                tempTR = order.GetOrders(User_id);
                tempTR2 = productRates.GetProductRatingsWUserID(int.Parse(Session["idUser"].ToString()));

                if (!tempTR.isError)
                {
                    foreach (DataRow datarow in tempTR.ordersTable.Tables[0].Rows)
                    {
                        if (tempTR2.productRatingsTable.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow datarow2 in tempTR2.productRatingsTable.Tables[0].Rows)
                            {
                                if (datarow["Product_id"].ToString() == datarow2["Product_id"].ToString())
                                {
                                    order = MakeOrders(datarow, int.Parse(datarow2["Rating"].ToString()));

                                    if (order.id != int.Parse(datarow2["Order_id"].ToString()))
                                    {
                                        order.Rating = 0;
                                    }

                                    booleanCheck = orders.Any(memberOrder => memberOrder.id == order.id);

                                    if (!booleanCheck)
                                    {
                                        orders.Add(order);
                                    }
                                    else
                                    {
                                        booleanCheck = orders.Any(memberOrder => (memberOrder.id == order.id) && (memberOrder.Rating == 0));

                                        if (booleanCheck)
                                        {
                                            var objToRemove = orders.Single(memberOrder => (memberOrder.id == order.id) && (memberOrder.Rating == 0));
                                            orders.Remove(objToRemove);
                                            orders.Add(order);
                                        }
                                    }
                                }
                                else
                                {
                                    order = MakeOrders(datarow, 0);
                                    booleanCheck = orders.Any(memberOrder => memberOrder.id == order.id);

                                    if (!booleanCheck)
                                    {
                                        orders.Add(order);
                                    }
                                }
                            }
                        }
                        else 
                        {
                            order = MakeOrders(datarow, 0);
                            orders.Add(order);
                        }
                    }
                }

                OrdersRepeater.DataSource = orders;
                OrdersRepeater.DataBind();
            }
        }
    }
}