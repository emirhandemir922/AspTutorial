using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerce
{
    public class CommentsWLikes
    {
        public int id { get; set; }
        public string Comment { get; set; }
        public string Commentor { get; set; }
        public int User_id { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Liked { get; set; }
        public int Disliked { get; set; }
    }
}