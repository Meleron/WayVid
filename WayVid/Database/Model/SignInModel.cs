﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Database.Model
{
    public class SignInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
