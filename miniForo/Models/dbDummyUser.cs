using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniForo.Models
{
    public class dbDummyUser
    {
        public string name { set; get; }

        public string email { set; get; }

        public string creationDate { set; get; }
        public int karma { set; get; }

        public string password { set; get; }
      
    }
}