using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public string UserAgent { get; set; }
    }
}