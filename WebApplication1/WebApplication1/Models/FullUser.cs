using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FullUser
    {
        public User user { get; set; }
        public Role role { get; set; }
    }
}