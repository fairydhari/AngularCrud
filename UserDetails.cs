using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace webapieg.Models
{
    public class UserDetails
    {
        public int userId{get;set;}
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string mobilephone { get; set; }
    }
}