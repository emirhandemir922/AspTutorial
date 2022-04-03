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
using System.Text;

namespace AspTutorial 
{
    public class Products
    {
        public int id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int Rates { get; set; }
        public int TotalRate { get; set; }
        public int Memory { get; set; }
        public double Screen { get; set; }
        public int Battery { get; set; }
        public double Processor { get; set; }
        public int Camera { get; set; }
        public bool Favorite { get; set; }
        public string Sponsor { get; set; }
        public double SearchPercentage { get; set; }
        public string SimilarProductNames { get; set; }
        public int Stock { get; set; }
        public int Sales { get; set; }
        public int SponsorRate { get; set; }
        public int Seller_id { get; set; }

        TResult tr = new TResult();
        static string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(CS);
        MySqlCommand command = new MySqlCommand();
        List<Products> products = new List<Products>();
        int ProductCount;
        string[] ParallelsArr;
        char[] whitespace = new char[] { ' ', '\t' };

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
            tempProduct.SimilarProductNames = row["SimilarProductNames"].ToString();
            tempProduct.SearchPercentage = 0;
            tempProduct.SponsorRate = int.Parse(row["SponsorRate"].ToString());

            return tempProduct;
        }

        public int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        public double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        public TResult InsertInto(Products newProduct)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO products(ProductName, Category, Brand, Price, Photo, Description, Rating, Rates, Memory, Screen, Battery, Processor, Camera) VALUES(@Name, @Category, @Brand, @Price, @Photo, @Description, @Rating, @Rates, @Memory, @Screen, @Battery, @Processor, @Camera);", connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@Name", newProduct.ProductName);
                    command.Parameters.AddWithValue("@Category", newProduct.Category);
                    command.Parameters.AddWithValue("@Brand", newProduct.Brand);
                    command.Parameters.AddWithValue("@Price", newProduct.Price);
                    command.Parameters.AddWithValue("@Photo", newProduct.Photo);
                    command.Parameters.AddWithValue("@Description", newProduct.Description);
                    command.Parameters.AddWithValue("@Rating", newProduct.Rating);
                    command.Parameters.AddWithValue("@Rates", newProduct.Rates);
                    command.Parameters.AddWithValue("@Memory", newProduct.Memory);
                    command.Parameters.AddWithValue("@Screen", newProduct.Screen);
                    command.Parameters.AddWithValue("@Battery", newProduct.Battery);
                    command.Parameters.AddWithValue("@Processor", newProduct.Processor);
                    command.Parameters.AddWithValue("@Camera", newProduct.Camera);
                    command.ExecuteNonQuery();
                    tr.message = "done";
                    command.Connection.Close();
                }
                return tr;
            }
            catch(Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }
        
        public TResult GetProducts(Filters filters, string SelectedSorting, string Category, int Page)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.products ";

                    if (Category != "Main")
                    {
                        command += "WHERE (Category = '" + Category + "') ";
                    }

                    else 
                    {
                        command += "WHERE (Category LIKE '%') ";
                    }

                    if (filters != null) 
                    {
                        if (filters.Brand != "Main")
                        {
                            string[] brands = filters.Brand.Split('-');
                            brands = brands.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            command += "AND (Brand IN (";

                            for (int i = 0; i < brands.Length; i++)
                            {
                                if (i + 1 == brands.Length)
                                {
                                    command += "'" + brands[i] + "'";
                                }
                                else 
                                {
                                    command += "'" + brands[i] + "', ";
                                }
                            }

                            command += ")) ";
                        }

                        else 
                        {
                            command += "AND (Brand LIKE '%') ";
                        }

                        if (filters.MinPrice != 0) 
                        {
                            command += "AND (Price >= " + filters.MinPrice + ") ";
                        }

                        if (filters.MaxPrice != 0) 
                        {
                            command += "AND (Price <= " + filters.MaxPrice + ") ";
                        }

                        if (filters.Memory != 99)
                        {
                            string[] memories = filters.EmptyString.Split('-');
                            memories = memories.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            command += "AND (Memory IN (";

                            for (int i = 0; i < memories.Length; i++)
                            {
                                if (i + 1 == memories.Length && memories[i] != null && memories[i] != "")
                                {
                                    command += "'" + memories[i] + "'";
                                }
                                else
                                {
                                    command += "'" + memories[i] + "', ";
                                }
                            }

                            command += ")) ";
                        }

                        if (filters.MinScreen != 0)
                        {
                            string minScreen = filters.MinScreen.ToString();
                            minScreen = minScreen.Replace(',', '.');
                            command += "AND (Screen >= " + minScreen + ") ";
                        }

                        if (filters.MaxScreen != 0)
                        {
                            string maxScreen = filters.MaxScreen.ToString();
                            maxScreen = maxScreen.Replace(',', '.');
                            command += "AND (Screen <= " + maxScreen + ") ";
                        }
                    }

                    if (SelectedSorting != null)
                    {
                        if (SelectedSorting == "Default")
                        {
                            command += "ORDER BY id ASC ";
                        }
                        else if (SelectedSorting == "PriceLH")
                        {
                            command += "ORDER BY Price ASC ";
                        }
                        else if (SelectedSorting == "PriceHL")
                        {
                            command += "ORDER BY Price DESC ";
                        }
                        else if (SelectedSorting == "Name")
                        {
                            command += "ORDER BY ProductName ASC ";
                        }
                        else if (SelectedSorting == "Popularity")
                        {
                            command += "ORDER BY Rating DESC ";
                        }
                        else if (SelectedSorting == "HighSales")
                        {
                            command += "ORDER BY Sales DESC ";
                        }
                    }
                    else 
                    {
                        if (Category == "Main")
                        {
                            command += "ORDER BY SponsorRate DESC ";
                        }
                    }

                    string command2 = command + ";";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command2, connection);
                    adapter.Fill(tr.outputTable, "products");
                    Page -= 1;
                    ProductCount = Page * 9;
                    command += "LIMIT " + ProductCount.ToString() + ", 9;";
                    adapter = new MySqlDataAdapter(command, connection);
                    adapter.Fill(tr.productsTable, "products");
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

        public TResult GetProductCount() 
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    string command = "SELECT COUNT(*) FROM u7408250_onder.products";

                    MySqlCommand cmd = new MySqlCommand(command, connection);
                    tr.message = cmd.ExecuteScalar().ToString();

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
        public TResult SearchProduct(string searchValue)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.products;";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProductName", searchValue.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@Category", searchValue.ToString());

                    adapter.Fill(tr.searchTable, "products");
                }

                foreach (DataRow datarow in tr.searchTable.Tables[0].Rows)
                {
                    Products memberProduct = new Products();
                    memberProduct = MakeProducts(datarow);
                    searchValue = searchValue.Replace(" ", "");

                    ParallelsArr = memberProduct.SimilarProductNames.Split(whitespace);
                    memberProduct.SearchPercentage = CalculateSimilarity(searchValue, memberProduct.ProductName);

                    if (memberProduct.SearchPercentage > 0.2)
                    {
                        products.Add(memberProduct);
                    }

                    else
                    {
                        for (int i = 0; i < ParallelsArr.Length; i++)
                        {
                            memberProduct.SearchPercentage = CalculateSimilarity(searchValue, ParallelsArr[i]);

                            if (memberProduct.SearchPercentage > 0.5)
                            {
                                products.Add(memberProduct);
                                i = ParallelsArr.Length;
                            }
                        }
                    }
                }

                products.Sort((x, y) => y.SearchPercentage.CompareTo(x.SearchPercentage));
                tr.filteredProducts = products;
                return tr;
            }
            catch (Exception ex)
            {
                tr.message = ex.ToString();
                tr.isError = true;
                return tr;
            }
        }

        public TResult IncreaseRates(int id, double rating)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "UPDATE u7408250_onder.products SET TotalRate = TotalRate + @rating, Rates = Rates + 1, Rating = (TotalRate / Rates) WHERE id = @id;"
                        , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@rating", Convert.ToInt32(rating));
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

        public TResult DecreaseStock(string productName, int quantity)
        {
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                        "UPDATE u7408250_onder.products SET Stock = Stock - " + quantity.ToString() + " WHERE ProductName = @ProductName;"
                        , connection);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@ProductName", productName);
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
        } //When order is placed stock is reduced

        public TResult UpdateProducts(int id, string ProductName, string Category, string Brand, double Price, string Photo, string Description, double Rating, int Rates, int TotalRate, int Stock)
        {
            try
            {
                using (connection)
                {
                    string command = "UPDATE u7408250_onder.products SET ProductName = @Name, Category = @Category, Brand = @Brand, Price = @Price, Photo = @Photo, Description = @Description, Rating = @Rating, Rates = @Rates, Stock = @Stock WHERE id = @id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Name", ProductName);
                    adapter.SelectCommand.Parameters.AddWithValue("@Category", Category);
                    adapter.SelectCommand.Parameters.AddWithValue("@price", Price);
                    adapter.SelectCommand.Parameters.AddWithValue("@Photo", Photo);
                    adapter.SelectCommand.Parameters.AddWithValue("@Description", Description);
                    adapter.SelectCommand.Parameters.AddWithValue("@Brand", Brand);
                    adapter.SelectCommand.Parameters.AddWithValue("@Rating", Rating);
                    adapter.SelectCommand.Parameters.AddWithValue("@Rates", Rates);
                    adapter.SelectCommand.Parameters.AddWithValue("@TotalRate", TotalRate);
                    adapter.SelectCommand.Parameters.AddWithValue("@Stock", Stock);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id);
                    adapter.Fill(tr.outputTable, "products");
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

        public TResult GetProductID(string ProductName, string Category, double Price, string Description)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT* FROM u7408250_onder.products WHERE ProductName = @ProductName AND Category = @Category AND Price = @Price AND Description = @Description;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProductName", ProductName);
                    adapter.SelectCommand.Parameters.AddWithValue("@Category", Category);
                    adapter.SelectCommand.Parameters.AddWithValue("@Price", Price);
                    adapter.SelectCommand.Parameters.AddWithValue("@Description", Description);
                    adapter.Fill(tr.productsTable, "products");
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
        public TResult SelectByName(string ProductName)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.products WHERE ProductName = @ProductName;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProductName", ProductName);
                    adapter.Fill(tr.productsTable, "products");
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
        public TResult SelectByID(int id)
        {
            try
            {
                using (connection)
                {
                    string command = "SELECT * FROM u7408250_onder.products WHERE id = @id;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id);
                    adapter.Fill(tr.productsTable, "products");
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
        public TResult DeleteByID(int id)
        {
            try
            {
                using (connection)
                {
                    command = new MySqlCommand(
                        "Delete FROM products WHERE id = @id;"
                    , connection);

                    connection.Open();
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