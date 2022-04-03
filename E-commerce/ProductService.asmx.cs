using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using E_commerce;

namespace AspTutorial
{
    /// <summary>
    /// WebService1 için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        TResult tempTR = new TResult();
        TResult mainTR = new TResult();
        Products newProduct = new Products();
        Image newImage = new Image();
        Comments newComment = new Comments();
        User newUser = new User();
        Filters newFilter = new Filters();
        Orders newOrder = new Orders();
        CommentLikes newCommentLikes = new CommentLikes();
        CommentsWLikes newCommentWLikes = new CommentsWLikes();
        ProductRatings newProductRatings = new ProductRatings();
        ProductFavorite newProductFavorite = new ProductFavorite();
        CartItems newCartProducts = new CartItems();
        List<Products> productsList = new List<Products>();
        List<ProductFavorite> productFavorites = new List<ProductFavorite>();
        List<CartItems> cartProducts = new List<CartItems>();

        int Product_id;
        string outputJSON;
        DataRow datarow;

        private Products MakeProducts(DataRow row)
        {
            Products tempProduct = new Products();

            tempProduct.id = int.Parse(row["id"].ToString());
            tempProduct.ProductName = row["ProductName"].ToString();
            tempProduct.Category = row["Category"].ToString();
            tempProduct.Brand = row["Brand"].ToString();
            tempProduct.Price = double.Parse(row["Price"].ToString());
            tempProduct.Photo = row["Photo"].ToString();
            tempProduct.Description = row["Description"].ToString();
            tempProduct.Rating = double.Parse(row["Rating"].ToString());
            tempProduct.Rates = int.Parse(row["Rates"].ToString());
            tempProduct.Favorite = false;
            tempProduct.Stock = int.Parse(row["Stock"].ToString());

            return tempProduct;
        }
        private User MakeUser(DataRow row)
        {
            User tempUser = new User();

            tempUser.id = int.Parse(row["id"].ToString());
            tempUser.Name = row["Name"].ToString();
            tempUser.SurName = row["SurName"].ToString();
            tempUser.Email = row["Email"].ToString();
            tempUser.Password = null;
            tempUser.Type = row["Type"].ToString();

            return tempUser;
        }
        private CartItems MakeCartProducts(DataRow row)
        {
            CartItems tempProduct = new CartItems();

            tempProduct.id = int.Parse(row["id"].ToString());
            tempProduct.Product_id = int.Parse(row["Product_id"].ToString());
            tempProduct.ProductName = row["ProductName"].ToString();
            tempProduct.Price = int.Parse(row["Price"].ToString());
            tempProduct.User_id = int.Parse(row["User_id"].ToString());
            tempProduct.Quantity = int.Parse(row["Quantity"].ToString());
            tempProduct.Photo = row["Photo"].ToString();

            return tempProduct;
        }
        private ProductFavorite MakeProductFavorites(DataRow row)
        {
            ProductFavorite tempProductFavorite = new ProductFavorite();

            tempProductFavorite.idProductFavorite = int.Parse(row["id"].ToString());
            tempProductFavorite.User_id = int.Parse(row["User_id"].ToString());
            tempProductFavorite.Product_id = int.Parse(row["Product_id"].ToString());

            return tempProductFavorite;
        }

        public void DivideCommentWLikes(CommentsWLikes CommentWLikes, int User_id)
        {
            newComment.id = CommentWLikes.id;
            newComment.Comment = CommentWLikes.Comment;
            newComment.Commentor = CommentWLikes.Commentor;
            newComment.Likes = CommentWLikes.Likes;
            newComment.Dislikes = CommentWLikes.Dislikes;
            newComment.User_id = 0;

            newCommentLikes.id = 0;
            newCommentLikes.Comment_id = CommentWLikes.id;
            newCommentLikes.User_id = User_id;
            newCommentLikes.Liked = CommentWLikes.Liked;
            newCommentLikes.Disliked = CommentWLikes.Disliked;
        }

        [WebMethod(EnableSession = true)]
        public void DeleteSessionData()
        {
            Session.Clear();
        }

        [WebMethod(EnableSession = true)]
        public User GetSessionData() 
        {
            if (Session["Email"] == "" || Session["Email"] == null)
            {
                newUser = null;
            }
            else 
            {
                newUser.Email = Session["Email"].ToString();
                tempTR = newUser.SelectUserByMail(newUser.Email);
                datarow = tempTR.user.Tables[0].Rows[0];
                newUser = MakeUser(datarow);
            }

            return newUser;
        }

        [WebMethod]
        public string Autocomplete(string searchValue) 
        {
            mainTR = newProduct.SearchProduct(searchValue);
            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string ShowImages(int Product_id)
        {
            tempTR = newImage.ShowImages(Product_id);
            mainTR = tempTR;

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR.imageTable, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string AddComment(string comment, int Product_id)
        {
            newUser = GetSessionData();

            if (newUser == null)
            {
                mainTR.isError = true;
                mainTR.message = "relocate to login";

                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
            else
            {
                tempTR = newOrder.CheckOrder(newUser.id, Product_id);

                if (tempTR.isError)
                {
                    mainTR.isError = true;
                    mainTR.message = "You have to purchase the product or wait for the shipment to be delivered before making a comment";
                    outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                    return outputJSON;
                }

                else
                {
                    tempTR = newComment.CheckComment(newUser.id, newProduct.id);

                    if (tempTR.isError)
                    {
                        if (tempTR.message == null)
                        {
                            mainTR.isError = true;
                            mainTR.message = "Please edit your previously made comment instead of writing a new one";
                            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                            return outputJSON;
                        }
                        else
                        {
                            mainTR.isError = true;
                            mainTR.message = "Something went wrong with database, please try again later";
                            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                            return outputJSON;
                        }
                    }
                    else
                    {
                        newComment.id = 0;
                        newComment.Comment = comment;
                        newComment.Commentor = newUser.Name;
                        newComment.Likes = 0;
                        newComment.Dislikes = 0;
                        newComment.Product_id = Product_id;
                        newComment.User_id = newUser.id;

                        tempTR = newComment.AddComment(newComment.Comment, newComment.Commentor, newComment.Likes, newComment.Dislikes, newComment.Product_id, newComment.User_id);
                        tempTR = newComment.ShowComments(newComment.Product_id);
                        mainTR = tempTR;
                        outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                        return outputJSON;
                    }
                }
            }
        }

        [WebMethod]
        public string ShowComments(int Product_id)
        {
            tempTR = newComment.ShowComments(Product_id);
            mainTR = tempTR;

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR.commentsTable, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string SelectComment(int Product_id)
        {
            tempTR = newComment.SelectComment(Product_id);
            mainTR = tempTR;

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR.commentsTable, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string CommentLike(string values)
        {
            newUser = GetSessionData();

            if (newUser == null)
            {
                mainTR.isError = true;
                mainTR.message = "relocate to login";

                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
            else
            {
                newCommentWLikes = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentsWLikes>(values);
                DivideCommentWLikes(newCommentWLikes, newUser.id);

                tempTR = newCommentLikes.SelectCommentLike(newCommentLikes.User_id, newComment.id);
                if (tempTR.commentLikesTable.Tables[0].Rows.Count > 0)
                {
                    mainTR.isError = true;
                    mainTR.message = "unlock dislike button";
                    newCommentLikes.DeleteCommentLike(newCommentLikes.User_id, newComment.id);
                    newComment.DecreaseDislike(newComment.id);
                }

                tempTR = newCommentLikes.LikeComment(newCommentLikes.User_id, newComment.id, newCommentLikes.Liked, newCommentLikes.Disliked);

                tempTR = newComment.IncreaseLike(newComment.id);

                mainTR.commentLikesTable = tempTR.commentLikesTable;

                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        [WebMethod]
        public string CommentDislike(string values)
        {
            newUser = GetSessionData();

            if (newUser == null)
            {
                mainTR.isError = true;
                mainTR.message = "relocate to login";

                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
            else
            {
                newCommentWLikes = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentsWLikes>(values);
                DivideCommentWLikes(newCommentWLikes, newUser.id);

                tempTR = newCommentLikes.SelectCommentLike(newCommentLikes.User_id, newComment.id);
                if (tempTR.commentLikesTable.Tables[0].Rows.Count > 0)
                {
                    mainTR.isError = true;
                    mainTR.message = "unlock like button";
                    newCommentLikes.DeleteCommentLike(newCommentLikes.User_id, newComment.id);
                    newComment.DecreaseLike(newComment.id);
                }

                tempTR = newCommentLikes.DislikeComment(newCommentLikes.User_id, newComment.id, newCommentLikes.Liked, newCommentLikes.Disliked);

                tempTR = newComment.IncreaseDislike(newComment.id);

                mainTR.commentLikesTable = tempTR.commentLikesTable;

                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        [WebMethod]
        public string GetCommentLikes(int Comment_id)
        {
            tempTR = newCommentLikes.GetCommentLikes(Comment_id);
            mainTR = tempTR;

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR.commentLikesTable, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string GetProductID(string values)
        {
            newProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<Products>(values);
            newProduct.Category = GetCategory(int.Parse(newProduct.Category));

            tempTR = newProduct.GetProductID(newProduct.ProductName, newProduct.Category, newProduct.Price, newProduct.Description);
            mainTR = tempTR;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return json;
        }

        [WebMethod]
        public string UpdateProduct(string values)
        {
            newProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<Products>(values);

            tempTR = newProduct.UpdateProducts(newProduct.id, newProduct.ProductName, newProduct.Category, newProduct.Brand, newProduct.Price, newProduct.Photo, newProduct.Description, newProduct.Rating, newProduct.Rates, newProduct.TotalRate, newProduct.Stock);
            mainTR = tempTR;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return json;
        }

        [WebMethod]
        public string RateProduct(int rating, int Product_id, int Order_id)
        {
            newUser = GetSessionData();

            newProductRatings.Product_id = Product_id;
            newProductRatings.Rating = rating;
            newProductRatings.User_id = newUser.id;
            newProductRatings.Order_id = Order_id;
            newProductRatings.RatingDateTime = DateTime.Now;

            tempTR = newProductRatings.RateProduct(newProductRatings.User_id, newProductRatings.Product_id, newProductRatings.Order_id, newProductRatings.Rating, newProductRatings.RatingDateTime);
            tempTR = newProduct.IncreaseRates(newProductRatings.Product_id, newProductRatings.Rating);
            mainTR.message = "Successfull call";
            mainTR.isError = false;

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string CheckStock(string productName) 
        {
            tempTR = newProduct.SelectByName(productName);
            newProduct = MakeProducts(tempTR.productsTable.Tables[0].Rows[0]);

            if (!tempTR.isError)
            {
                mainTR.isError = false;
                mainTR.message = newProduct.Stock.ToString();
            }
            else 
            {
                mainTR.isError = false;
                mainTR.message = tempTR.message;
            }

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string AddProduct(string values)
        {
            newProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<Products>(values);
            newProduct.Category = GetCategory(int.Parse(newProduct.Category));
            newProduct.Brand = GetBrand(int.Parse(newProduct.Brand));
            newProduct.Memory = GetMemory(newProduct.Memory);
            tempTR = newProduct.SelectByName(newProduct.ProductName);

            if (tempTR.productsTable.Tables[0].Rows.Count > 0)
            {
                tempTR.isError = true;
            }
            else
            {
                tempTR = newProduct.InsertInto(newProduct);
            }

            mainTR = tempTR;
            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string DeleteProduct(int id)
        {
            tempTR = newProduct.DeleteByID(id);
            mainTR = tempTR;

            outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
            return outputJSON;
        }

        [WebMethod]
        public string GetCategory(int Category_id)
        {
            if (Category_id == 1)
            {
                return "Screen";
            }
            else if (Category_id == 2)
            {
                return "Hardware";
            }
            else if (Category_id == 3)
            {
                return "Keyboard & Mouse";
            }
            else
            {
                return "Gaming";
            }
        }

        [WebMethod]
        public string GetBrand(int Brand_id)
        {
            if (Brand_id == 1)
            {
                return "SAMSUNG";
            }
            else if (Brand_id == 2)
            {
                return "Apple";
            }
            else
            {
                return "Xiamoi";
            }
        }

        [WebMethod]
        public int GetMemory(int Memory_id)
        {
            if (Memory_id == 1)
            {
                return 16;
            }
            else if (Memory_id == 2)
            {
                return 32;
            }
            else if (Memory_id == 3)
            {
                return 64;
            }
            else if (Memory_id == 4)
            {
                return 128;
            }
            else if (Memory_id == 5)
            {
                return 256;
            }
            else
            {
                return 512;
            }
        }

        [WebMethod]
        public void EditUser(string values)
        {
            newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(values);
            tempTR = newUser.EditUser(newUser.id, newUser.Name, newUser.SurName, newUser.Email, newUser.Password);
        }

        [WebMethod]
        public string AddProductFavorite(string ProductName)
        {
            newUser = GetSessionData();

            if (newUser == null)
            {
                mainTR.message = "Redirect to Login";
                mainTR.isError = true;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
            else
            {
                tempTR = newProduct.SelectByName(ProductName);
                DataRow datarow = tempTR.productsTable.Tables[0].Rows[0];
                Product_id = int.Parse(datarow["id"].ToString());

                tempTR = newProductFavorite.AddFavoriteProduct(newUser.id, Product_id);
                mainTR = newProductFavorite.GetProductFavorites(newUser.Email);

                foreach (DataRow row in mainTR.favoriteProducts.Tables[0].Rows)
                {
                    productFavorites.Add(MakeProductFavorites(row));
                }

                for (int i = 0; i < productFavorites.Count; i++)
                {
                    tempTR = newProduct.SelectByID(productFavorites[i].Product_id);
                    datarow = tempTR.productsTable.Tables[0].Rows[i];
                    productsList.Add(MakeProducts(datarow));
                }

                mainTR.favoriteProductsList = productsList;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        [WebMethod]
        public string RemoveProductFavorite(string ProductName)
        {
            newUser = GetSessionData();

            if (newUser.Email == null)
            {
                mainTR.message = "Redirect to Login";
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
            else
            {
                tempTR = newProduct.SelectByName(ProductName);
                datarow = tempTR.productsTable.Tables[0].Rows[0];
                Product_id = int.Parse(datarow["id"].ToString());
                tempTR.productsTable.Clear();

                tempTR = newProductFavorite.RemoveFavoriteProduct(newUser.id, Product_id);
                mainTR = newProductFavorite.GetProductFavorites(newUser.Email);
                foreach (DataRow row in mainTR.favoriteProducts.Tables[0].Rows)
                {
                    productFavorites.Add(MakeProductFavorites(row));
                }

                for (int i = 0; i < productFavorites.Count; i++)
                {
                    tempTR = newProduct.SelectByID(productFavorites[i].Product_id);
                    datarow = tempTR.productsTable.Tables[0].Rows[i];
                    productsList.Add(MakeProducts(datarow));
                }

                mainTR.favoriteProductsList = productsList;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        [WebMethod(EnableSession = true)]
        public string GetProductFavorites()
        {
            newUser = GetSessionData();

            if (newUser != null)
            {
                tempTR = newProductFavorite.GetProductFavorites(newUser.Email);

                if (tempTR.favoriteProducts.Tables.Contains("favoriteproducts"))
                {
                    if (tempTR.favoriteProducts.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow row in tempTR.favoriteProducts.Tables[0].Rows)
                        {
                            productFavorites.Add(MakeProductFavorites(row));
                        }

                        for (int i = 0; i < productFavorites.Count; i++)
                        {
                            tempTR = newProduct.SelectByID(productFavorites[i].Product_id);

                            if (!tempTR.isError)
                            {
                                datarow = tempTR.productsTable.Tables[0].Rows[i];
                                tempTR.favoriteProductsList.Add(MakeProducts(datarow));
                            }
                            else
                            {
                                mainTR.message = "Error in DB";
                                break;
                            }
                        }

                        mainTR = tempTR;
                        outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                        return outputJSON;
                    }

                    else
                    {
                        mainTR.isError = true;
                        outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                        return outputJSON;
                    }
                }
                else
                {
                    mainTR.isError = true;
                    outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                    return outputJSON;
                }
            }
            else
            {
                mainTR.isError = true;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        //[WebMethod]
        //public string FilterProducts(string filters) 
        //{
        //    newFilter = Newtonsoft.Json.JsonConvert.DeserializeObject<Filters>(filters);
        //    mainTR = newFilter.FilterProducts(newFilter); 

        //    outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
        //    return outputJSON;
        //}

        [WebMethod]
        public string AddOrder()
        {
            newUser = GetSessionData();
            DateTime Now = DateTime.Now;

            tempTR = newCartProducts.GetShoppingCart(newUser.id);
            foreach (DataRow row in tempTR.shoppingTable.Tables[0].Rows)
            {
                cartProducts.Add(MakeCartProducts(row));
            }

            for (int i = 0; i < cartProducts.Count; i++)
            {
                newOrder.AddOrder(cartProducts[i].Product_id, cartProducts[i].ProductName, cartProducts[i].Photo, cartProducts[i].Seller_id, newUser.id, cartProducts[i].Quantity, cartProducts[i].Quantity * cartProducts[i].Price, "Waiting for shipment", Now);
            }

            return "Order received successfully";
        }

        [WebMethod(EnableSession = true)]
        public string GetShoppingCart()
        {
            string json;
            string ProductName;
            int ProductsCount;
            int ProductsQuantity;
            int ProductQuantity;
            string ProductMessage;

            newUser = GetSessionData();

            if (newUser == null)
            {
                mainTR.isError = true;
                HttpCookie cartInfo = null;

                if (HttpContext.Current.Request.Cookies.AllKeys.Contains("CartInfo"))
                {
                    cartInfo = HttpContext.Current.Request.Cookies["CartInfo"];
                    ProductsCount = int.Parse(cartInfo["ProductsCount"]);
                    ProductsQuantity = int.Parse(cartInfo["TotalQuantity"]);

                    for (int i = 0; i <= ProductsCount; i++)
                    {
                        if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                        {
                            HttpCookie cookieProduct = HttpContext.Current.Request.Cookies["Product" + i];
                            ProductName = cookieProduct["ProductName" + i];
                            ProductQuantity = int.Parse(cookieProduct["Quantity"]);
                            ProductMessage = cookieProduct["Message"];

                            tempTR = newProduct.SelectByName(ProductName);
                            DataRow datarow = tempTR.productsTable.Tables[0].Rows[0];
                            newProduct = MakeProducts(datarow);

                            if (ProductMessage != null)
                            {
                                newProduct.Description = ProductMessage;
                            }
                            else 
                            {
                                newProduct.Description = "";
                            }

                            for (int j = 0; j < ProductQuantity; j++)
                            {
                                productsList.Add(newProduct);
                            }
                        }
                    }

                    mainTR.message = "continue";
                    mainTR.shoppingCartProducts = productsList;
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                    return json;
                }
                else 
                {
                    mainTR.message = "no cookie";
                    outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                    return outputJSON;
                }
            }
            else 
            {
                mainTR.isError = false;

                tempTR = newCartProducts.GetShoppingCart(newUser.id);
                foreach (DataRow row in tempTR.shoppingTable.Tables[0].Rows)
                {
                    cartProducts.Add(MakeCartProducts(row));
                }

                for (int i = 0; i < cartProducts.Count; i++)
                {
                    tempTR = newProduct.SelectByID(cartProducts[i].Product_id);
                    datarow = tempTR.productsTable.Tables[0].Rows[i];

                    if (cartProducts[i].Quantity > 0)
                    {
                        for (int j = 0; j < cartProducts[i].Quantity; j++)
                        {
                            productsList.Add(MakeProducts(datarow));
                        }
                    }
                }

                mainTR.message = "continue";
                mainTR.shoppingCartProducts = productsList;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        [WebMethod(EnableSession = true)]
        public string AddShoppingCart(int Product_id, int Quantity)
        {
            newUser = GetSessionData();

            if (newUser == null)
            {
                int i;
                int ProductsCount = 0;
                int ProductsQuantity;
                int ProductQuantity;
                string ProductName;
                bool createNew = true;
                mainTR.isError = true;
                mainTR.message = "continue";

                HttpCookie cartInfo = null;

                if (HttpContext.Current.Request.Cookies.AllKeys.Contains("CartInfo"))
                {
                    cartInfo = HttpContext.Current.Request.Cookies["CartInfo"];
                    cartInfo.Expires = DateTime.Now.AddDays(7);
                    ProductsCount = int.Parse(cartInfo["ProductsCount"]);
                    ProductsCount += 1;
                    ProductsQuantity = int.Parse(cartInfo["TotalQuantity"]);
                    ProductsQuantity += Quantity;
                    cartInfo["TotalQuantity"] = ProductsQuantity.ToString();
                    HttpContext.Current.Response.Cookies.Add(cartInfo);
                }
                else
                {
                    cartInfo = new HttpCookie("CartInfo");
                    cartInfo.Expires = DateTime.Now.AddDays(7);
                    ProductsQuantity = Quantity;
                    cartInfo["ProductsCount"] = ProductsCount.ToString();
                    cartInfo["TotalQuantity"] = ProductsQuantity.ToString();
                    HttpContext.Current.Response.Cookies.Add(cartInfo);
                }

                HttpCookie shoppingCartProduct = null;

                tempTR = newProduct.SelectByID(Product_id);
                datarow = tempTR.productsTable.Tables[0].Rows[0];
                newProduct = MakeProducts(datarow);

                for (i = 1; i <= ProductsCount; i++)
                {
                    if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                    {
                        shoppingCartProduct = HttpContext.Current.Request.Cookies["Product" + i];

                        if (shoppingCartProduct["ProductName" + i].ToString() == newProduct.ProductName)
                        {
                            shoppingCartProduct["Quantity"] = (int.Parse(shoppingCartProduct["Quantity"]) + Quantity).ToString();
                            shoppingCartProduct.Expires = DateTime.Now.AddDays(7);
                            HttpContext.Current.Response.Cookies.Add(shoppingCartProduct);
                            createNew = false;
                        }
                    }
                }

                if (createNew)
                {
                    ProductsCount += 1;
                    cartInfo["ProductsCount"] = ProductsCount.ToString();

                    shoppingCartProduct = new HttpCookie("Product" + ProductsCount);
                    shoppingCartProduct.Expires = DateTime.Now.AddDays(7);
                    HttpContext.Current.Response.Cookies.Add(shoppingCartProduct);

                    shoppingCartProduct["ProductName" + ProductsCount] = newProduct.ProductName;
                    shoppingCartProduct["Price" + ProductsCount] = newProduct.Price.ToString();
                    shoppingCartProduct["Rates" + ProductsCount] = newProduct.Rates.ToString();
                    shoppingCartProduct["Photo" + ProductsCount] = newProduct.Photo;
                    shoppingCartProduct["Quantity"] = Quantity.ToString();
                }

                for (i = 1; i <= ProductsCount; i++)
                {
                    if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                    {
                        HttpCookie cookieProduct = HttpContext.Current.Request.Cookies["Product" + i];
                        ProductName = cookieProduct["ProductName" + i];
                        ProductQuantity = int.Parse(cookieProduct["Quantity"]);

                        for (int j = 0; j < ProductQuantity; j++)
                        {
                            tempTR = newProduct.SelectByName(ProductName);
                            datarow = tempTR.productsTable.Tables[0].Rows[0];
                            newProduct = MakeProducts(datarow);
                            productsList.Add(newProduct);
                        }
                    }
                }

                mainTR.shoppingCartProducts = productsList;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
            else
            {
                tempTR = newProduct.SelectByID(Product_id);
                datarow = tempTR.productsTable.Tables[0].Rows[0];
                newProduct = MakeProducts(datarow);

                tempTR = newCartProducts.CheckShoppingCart(newUser.id, newProduct.id);

                if (tempTR.shoppingTable.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < tempTR.shoppingTable.Tables[0].Rows.Count; i++)
                    {
                        datarow = tempTR.shoppingTable.Tables[0].Rows[i];

                        if (datarow["ProductName"].ToString() == newProduct.ProductName)
                        {
                            tempTR = newCartProducts.IncreaseQuantity(newProduct.id, newUser.id, Quantity);
                            break;
                        }
                    }
                }

                else
                {
                    tempTR = newCartProducts.AddShoppingCart(newProduct.id, newProduct.ProductName, newProduct.Price, newUser.id, Quantity, newProduct.Photo, newProduct.Seller_id);
                }

                mainTR = newCartProducts.GetShoppingCart(newUser.id);

                foreach (DataRow row in mainTR.shoppingTable.Tables[0].Rows)
                {
                    cartProducts.Add(MakeCartProducts(row));
                }

                for (int i = 0; i < cartProducts.Count; i++)
                {
                    tempTR = newProduct.SelectByID(cartProducts[i].Product_id);
                    datarow = tempTR.productsTable.Tables[0].Rows[i];

                    if (cartProducts[i].Quantity > 0)
                    {
                        for (int j = 0; j < cartProducts[i].Quantity; j++)
                        {
                            productsList.Add(MakeProducts(datarow));
                        }
                    }
                }

                mainTR.shoppingCartProducts = productsList;
                mainTR.isError = false;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }

        [WebMethod(EnableSession = true)]
        public string RemoveShoppingCart(string ProductName)
        {
            int removeID = 0;
            int productID;
            int ProductsCount;
            int ProductQuantity;
            int ProductsQuantity;
            int loopCount;
            mainTR.isError = true;
            mainTR.message = "continue";
            HttpCookie cartInfo = null;

            newUser = GetSessionData();

            if (newUser == null)
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains("CartInfo"))
                {
                    HttpCookie shoppingCartProduct = null;

                    cartInfo = HttpContext.Current.Request.Cookies["CartInfo"];
                    ProductsCount = int.Parse(cartInfo["ProductsCount"]);
                    loopCount = ProductsCount;
                    ProductsQuantity = int.Parse(cartInfo["TotalQuantity"]);

                    for (int i = 1; i <= loopCount; i++)
                    {
                        bool threshold = false;
                        Products tempProduct = new Products();

                        if (HttpContext.Current.Request.Cookies.AllKeys.Contains("Product" + i))
                        {
                            shoppingCartProduct = HttpContext.Current.Request.Cookies["Product" + i];
                            tempProduct.ProductName = shoppingCartProduct["ProductName" + i];
                            tempProduct.Price = Double.Parse(shoppingCartProduct["Price" + i]);
                            tempProduct.Rates = int.Parse(shoppingCartProduct["Rates" + i]);
                            tempProduct.Photo = shoppingCartProduct["Photo" + i];
                            ProductQuantity = int.Parse(shoppingCartProduct["Quantity"]);

                            if (tempProduct.ProductName == ProductName)
                            {
                                removeID = i;
                                threshold = true;
                            }

                            if (i > removeID && threshold)
                            {
                                productID = i - removeID;

                                if (removeID == 0)
                                {
                                    productID -= 1;
                                }

                                HttpCookie newCookieProduct = new HttpCookie("Product" + productID);
                                newCookieProduct["ProductName" + productID] = tempProduct.ProductName;
                                newCookieProduct["Price" + productID] = tempProduct.Price.ToString();
                                newCookieProduct["Rates" + productID] = tempProduct.Rates.ToString();
                                newCookieProduct["Photo" + productID] = tempProduct.Photo;
                                newCookieProduct["Quantity"] = ProductQuantity.ToString();

                                shoppingCartProduct.Expires = DateTime.Now.AddDays(-30d);
                                newCookieProduct.Expires = DateTime.Now.AddDays(7d);

                                HttpContext.Current.Response.Cookies.Add(shoppingCartProduct);
                                HttpContext.Current.Response.Cookies.Add(newCookieProduct);
                            }
                            else if (i == removeID && threshold)
                            {
                                if (ProductQuantity > 1)
                                {
                                    ProductQuantity -= 1;
                                    shoppingCartProduct["Quantity"] = ProductQuantity.ToString();
                                    shoppingCartProduct.Expires = DateTime.Now.AddDays(7d);
                                    HttpContext.Current.Response.Cookies.Add(shoppingCartProduct);
                                }
                                else
                                {
                                    ProductQuantity -= 1;
                                    ProductsCount -= 1;
                                    shoppingCartProduct.Expires = DateTime.Now.AddDays(-30d);
                                    HttpContext.Current.Response.Cookies.Add(shoppingCartProduct);
                                }
                            }

                            for (int j = 0; j < ProductQuantity; j++)
                            {
                                tempTR = newProduct.SelectByName(tempProduct.ProductName);
                                DataRow datarow = tempTR.productsTable.Tables[0].Rows[0];
                                newProduct = MakeProducts(datarow);

                                productsList.Add(newProduct);
                            }
                        }
                    }

                    ProductsQuantity -= 1;
                    cartInfo["ProductsCount"] = ProductsCount.ToString();
                    cartInfo["TotalQuantity"] = ProductsQuantity.ToString();
                    HttpContext.Current.Response.Cookies.Add(cartInfo);

                    mainTR.shoppingCartProducts = productsList;
                    outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                    return outputJSON;
                }

                else
                {
                    mainTR.message = "Unexpected Error";
                    outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                    return outputJSON;
                }
            }
            else
            {
                bool remove = true;
                tempTR = newProduct.SelectByName(ProductName);
                DataRow datarow;
                tempTR = newCartProducts.GetShoppingCart(newUser.id);

                foreach (DataRow row in tempTR.shoppingTable.Tables[0].Rows)
                {
                    cartProducts.Add(MakeCartProducts(row));
                }

                for (int i = 0; i < cartProducts.Count; i++)
                {
                    if (cartProducts[i].Quantity > 0)
                    {
                        tempTR = newProduct.SelectByID(cartProducts[i].Product_id);
                        datarow = tempTR.productsTable.Tables[0].Rows[tempTR.productsTable.Tables[0].Rows.Count - 1];

                        for (int j = 1; j <= cartProducts[i].Quantity; j++)
                        {
                            if (cartProducts[i].ProductName == ProductName && remove)
                            {
                                newCartProducts.RemoveFromShoppingCart(cartProducts[i].Product_id, newUser.id);
                                remove = false;
                            }
                            else
                            {
                                productsList.Add(MakeProducts(datarow));
                            }
                        }
                    }
                }

                mainTR.shoppingCartProducts = productsList;
                mainTR.isError = false;
                outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(mainTR, Newtonsoft.Json.Formatting.Indented);
                return outputJSON;
            }
        }
    }
}
