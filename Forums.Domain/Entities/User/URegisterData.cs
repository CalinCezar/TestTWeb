﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.Domain.Entities.User
{
    public class URegisterData
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string InfoBlog { get; set; }

    }
}
