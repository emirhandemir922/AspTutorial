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
using System.IO;

namespace E_commerce
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        WebService1 service = new WebService1();
        Products product = new Products();
        ProductFavorite productfavorites = new ProductFavorite();
        AspTutorial.Image photos = new AspTutorial.Image();
        Comments comment = new Comments();
        User user = new User();
        ProductRatings productRate = new ProductRatings();
        CommentLikes commentLikes = new CommentLikes();
        ProductWLikes productWLikes = new ProductWLikes();
        CommentsWLikes commentWLikes = new CommentsWLikes();
        TResult tempTR = new TResult();
        TResult mainTR = new TResult();
        List<ProductRatings> productRates = new List<ProductRatings>();
        List<Comments> comments = new List<Comments>();
        List<CommentLikes> commentsLikes = new List<CommentLikes>();
        List<CommentsWLikes> commentsWLikes = new List<CommentsWLikes>();
        DataRow datarow;
        string ProductName;
        int counter = 0;

        private Comments MakeComments(DataRow row)
        {
            Comments tempComment = new Comments();

            tempComment.id = int.Parse(row["id"].ToString());
            tempComment.Comment = row["Comment"].ToString();
            tempComment.Commentor = row["Commentor"].ToString();
            tempComment.Likes = int.Parse(row["Likes"].ToString());
            tempComment.Dislikes = int.Parse(row["Dislikes"].ToString());
            tempComment.Product_id = int.Parse(row["Product_id"].ToString());
            tempComment.User_id = int.Parse(row["User_id"].ToString());

            return tempComment;
        }

        private CommentLikes MakeCommentsLikes(DataRow row)
        {
            CommentLikes tempCommentLike = new CommentLikes();

            tempCommentLike.id = int.Parse(row["id"].ToString());
            tempCommentLike.Comment_id = int.Parse(row["Comment_id"].ToString());
            tempCommentLike.User_id = int.Parse(row["User_id"].ToString());
            tempCommentLike.Liked = int.Parse(row["Liked"].ToString());
            tempCommentLike.Disliked = int.Parse(row["Disliked"].ToString());

            return tempCommentLike;
        }

        private ProductRatings MakeProductRatings(DataRow row)
        {
            ProductRatings tempProductRate = new ProductRatings();

            tempProductRate.id = int.Parse(row["id"].ToString());
            tempProductRate.User_id = int.Parse(row["User_id"].ToString());
            tempProductRate.Product_id = int.Parse(row["Product_id"].ToString());
            tempProductRate.Rating = int.Parse(row["Rating"].ToString());

            return tempProductRate;
        }

        private List<CommentsWLikes> CreateCommentsWLikes(List<Comments> comments, List<CommentLikes> commentLikes) 
        {
            List<CommentsWLikes> CommentsWLikes = new List<CommentsWLikes>();

            foreach (Comments memberComment in comments)
            {
                CommentsWLikes tempCommentsWLikes = new CommentsWLikes();

                tempCommentsWLikes.id = memberComment.id;
                tempCommentsWLikes.Comment = memberComment.Comment;
                tempCommentsWLikes.Commentor = memberComment.Commentor;
                tempCommentsWLikes.User_id = user.id;
                tempCommentsWLikes.Likes = memberComment.Likes;
                tempCommentsWLikes.Dislikes = memberComment.Dislikes;

                foreach (CommentLikes memberCommentLikes in commentLikes)
                {
                    if (tempCommentsWLikes.id == memberCommentLikes.Comment_id)
                    {
                        if (user.id == memberCommentLikes.User_id && memberCommentLikes.Liked == 1)
                        {
                            tempCommentsWLikes.Liked = 1;
                            tempCommentsWLikes.Disliked = 0;
                        }
                        else if (user.id == memberCommentLikes.User_id && memberCommentLikes.Disliked == 1)
                        {
                            tempCommentsWLikes.Liked = 0;
                            tempCommentsWLikes.Disliked = 1;
                        }
                        else
                        {
                            tempCommentsWLikes.Liked = 0;
                            tempCommentsWLikes.Disliked = 0;
                        }
                    }
                    else if (tempCommentsWLikes.User_id == 0)
                    {
                        tempCommentsWLikes.Liked = 0;
                        tempCommentsWLikes.Disliked = 0;
                    }
                }

                if (tempCommentsWLikes.Liked == null && tempCommentsWLikes.Disliked == null) 
                {
                    tempCommentsWLikes.Liked = 0;
                    tempCommentsWLikes.Disliked = 0;
                }

                CommentsWLikes.Add(tempCommentsWLikes);
            }

            return CommentsWLikes;
        }

        [WebMethod(EnableSession = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            user = service.GetSessionData();

            ProductName = Request.QueryString["Name"];

            tempTR = photos.SelectImages(ProductName);

            foreach (DataRow datarow in tempTR.imageTable.Tables[0].Rows) 
            {
                if (int.Parse(datarow["CoverPhoto"].ToString()) == 1)
                {
                    ProductPhoto.ImageUrl = datarow["PhotoSource"].ToString();
                    Image1.ImageUrl = datarow["PhotoSource"].ToString();
                    Image1Container.Attributes["data-imgbigurl"] = datarow["PhotoSource"].ToString();
                }
                else
                {
                    if (int.Parse(datarow["PhotoOrder"].ToString()) == 2)
                    {
                        Image2.ImageUrl = datarow["PhotoSource"].ToString();
                        Image2Container.Attributes["data-imgbigurl"] = datarow["PhotoSource"].ToString();
                    }
                    else if (int.Parse(datarow["PhotoOrder"].ToString()) == 3)
                    {
                        Image3.ImageUrl = datarow["PhotoSource"].ToString();
                        Image3Container.Attributes["data-imgbigurl"] = datarow["PhotoSource"].ToString();
                    }
                    else if (int.Parse(datarow["PhotoOrder"].ToString()) == 4)
                    {
                        Image4.ImageUrl = datarow["PhotoSource"].ToString();
                        Image4Container.Attributes["data-imgbigurl"] = datarow["PhotoSource"].ToString();
                    }
                    else if (int.Parse(datarow["PhotoOrder"].ToString()) == 5)
                    {
                        Image5.ImageUrl = datarow["PhotoSource"].ToString(); 
                        Image5Container.Attributes["data-imgbigurl"] = datarow["PhotoSource"].ToString();
                    }
                }
            }

            tempTR = product.SelectByName(ProductName);

            if (tempTR.productsTable.Tables[0].Rows.Count > 0)
            {
                datarow = tempTR.productsTable.Tables[0].Rows[0];

                productWLikes.id = int.Parse(datarow["id"].ToString());
                productWLikes.ProductName = datarow["ProductName"].ToString();
                productWLikes.Category = datarow["Category"].ToString();
                productWLikes.Brand = datarow["Brand"].ToString();
                productWLikes.Price = double.Parse(datarow["Price"].ToString());
                productWLikes.Photo = datarow["Photo"].ToString();
                productWLikes.Description = datarow["Description"].ToString();
                productWLikes.Rating = double.Parse(datarow["Rating"].ToString());
                productWLikes.Rates = int.Parse(datarow["Rates"].ToString());
                productWLikes.UserRating = 0;
                productWLikes.Rated = false;
                productWLikes.Stock = int.Parse(datarow["Stock"].ToString());

                if (productWLikes.Category == "Smartphone") 
                {
                    Gaming.Visible = false;
                    Accessories.Visible = false;
                    Headphones.Visible = false;

                    PriceSmartphone.InnerText = productWLikes.Price + " TL";
                    MemorySmartphone.InnerText = datarow["Memory"].ToString() + " GB";
                    ScreenSmartphone.InnerText = datarow["Screen"].ToString() + " inch";
                    BatterySmartphone.InnerText = datarow["Battery"].ToString() + " mAh";
                    ProcessorSmartphone.InnerText = datarow["Processor"].ToString() + " GHz";
                    CameraSmartphone.InnerText = datarow["Camera"].ToString() + " MP";
                }

                Session.Add("Product_id", productWLikes.id);

                if (user == null)
                {
                    user.id = 0;
                }

                tempTR = productRate.GetProductRatingsWProductID(productWLikes.id);

                foreach (DataRow datarow in tempTR.productRatingsTable.Tables[0].Rows)
                {
                    productRates.Add(MakeProductRatings(datarow));
                }

                foreach (ProductRatings member in productRates)
                {
                    if (member.User_id == user.id)
                    {
                        productWLikes.UserRating = member.Rating;
                        productWLikes.Rated = true;
                    }
                }

                ProductHeader.Text = productWLikes.ProductName;
                Description.Text = productWLikes.Description;
                Price.Text = productWLikes.Price + "TL";
                Rates.Text = productWLikes.Rates.ToString();
                Product_id.Text = productWLikes.id.ToString();
                TagsBrand.InnerText = productWLikes.Brand.ToString();
                TagsBrand.HRef = "Shop.aspx?category=Main&brand=" + productWLikes.Brand.ToString() + "&page=1";
                Category.InnerText = productWLikes.Category.ToString();
                Category.HRef = "Shop.aspx?category=" + productWLikes.Category.ToString() + "&page=1";

                if (productWLikes.Stock < 20)
                {
                    Stock.Text = "Running low!";
                }
                else if (productWLikes.Stock < 10) 
                {
                    Stock.Text = "Only" + productWLikes.ToString() + "left";
                }

                if (Convert.ToInt32(productWLikes.Rating) % 2 == 0)
                {
                    RatingImg.ImageUrl = "img/RatingStars/" + Convert.ToInt32(productWLikes.Rating) / 2 + ".jpg";
                }
                else
                {
                    RatingImg.ImageUrl = "img/RatingStars/" + ((Convert.ToInt32(productWLikes.Rating) - 1) / 2) + "-half.jpg";
                }
                
                tempTR = productfavorites.GetProductFavorites(user.Email);

                if (tempTR.favoriteProducts.Tables[0].Rows.Count != 0) 
                {
                    foreach (DataRow datarow in tempTR.favoriteProducts.Tables[0].Rows) 
                    {
                        if (int.Parse(datarow["Product_id"].ToString()) == productWLikes.id) 
                        {
                            ProductHeart.ImageUrl = "img/heart-full.jpg";
                        }
                    }
                }

                tempTR = comment.ShowComments(productWLikes.id);

                if (tempTR.commentsTable.Tables[0].Rows.Count == 0)
                {
                    CommentBtn.Text = "Comments (" + comments.Count.ToString() + ")";

                    CommentsRepeater.DataSource = commentsWLikes;
                    CommentsRepeater.DataBind();
                }
                else
                {
                    foreach (DataRow commentRow in tempTR.commentsTable.Tables[0].Rows)
                    {
                        comments.Add(MakeComments(commentRow));
                    }

                    tempTR = commentLikes.GetCommentLikes(user.id);

                    if (tempTR.commentLikesTable.Tables[0].Rows.Count == 0)
                    {
                        commentsLikes.Add(new CommentLikes
                        {
                            id = 0,
                            Comment_id = 0,
                            User_id = user.id,
                            Liked = 0,
                            Disliked = 0
                        });

                        commentsWLikes = CreateCommentsWLikes(comments, commentsLikes);

                        CommentBtn.Text = "Comments (" + comments.Count.ToString() + ")";

                        CommentsRepeater.DataSource = commentsWLikes;
                        CommentsRepeater.DataBind();
                    }
                    else
                    {
                        foreach (DataRow commentLikesRow in tempTR.commentLikesTable.Tables[0].Rows)
                        {
                            commentsLikes.Add(MakeCommentsLikes(commentLikesRow));
                        }

                        commentsWLikes = CreateCommentsWLikes(comments, commentsLikes);

                        CommentBtn.Text = "Comments (" + comments.Count.ToString() + ")";

                        CommentsRepeater.DataSource = commentsWLikes;
                        CommentsRepeater.DataBind();

                    }
                }
            }
            else 
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        protected void ProductHeart_Click(object sender, ImageClickEventArgs e)
        {
            if (ProductHeart.ImageUrl == "img/heart-full.jpg")
            {
                tempTR = productfavorites.RemoveFavoriteProduct(user.id, productWLikes.id);
            }
            else 
            {
                if (user.id == 0)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    tempTR = productfavorites.AddFavoriteProduct(user.id, productWLikes.id);
                }
            }

            if (tempTR.isError)
            {
                Response.Redirect("ErrorPage.aspx");
            }

            else 
            {
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}