using Forums.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forums.Web.Models
{
    public class UserData
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string InfoBlog { get; set; }
        public string Profession { get; set; }
        public string PhoneNumber { get; set; }
    }
}