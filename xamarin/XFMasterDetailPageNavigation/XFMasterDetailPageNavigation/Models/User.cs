﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XFMasterDetailPageNavigation.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}