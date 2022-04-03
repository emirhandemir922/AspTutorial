using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AspTutorial
{
    public class TResult
    {
        public bool isError = false;
        public string message;
        public DataSet user = new DataSet();
        public DataSet outputTable = new DataSet();
        public DataSet productsTable = new DataSet();
        public DataSet imageTable = new DataSet();
        public DataSet commentsTable = new DataSet();
        public DataSet commentLikesTable = new DataSet();
        public DataSet productRatingsTable = new DataSet();
        public DataSet searchTable = new DataSet();
        public DataSet favoriteProducts = new DataSet();
        public DataSet shoppingTable = new DataSet();
        public DataSet ordersTable = new DataSet();
        public List<Products> shoppingCartProducts = new List<Products>();
        public List<Products> favoriteProductsList = new List<Products>();
        public List<Products> filteredProducts = new List<Products>();
    }
}