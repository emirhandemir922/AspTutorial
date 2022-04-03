using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce
{
    public class ProductWLikes
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
        public int UserRating { get; set; }
        public bool Rated { get; set; }
        public int Stock { get; set; }
    }
}