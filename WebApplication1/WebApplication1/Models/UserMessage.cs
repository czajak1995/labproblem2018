using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserMessage
    {
        public string SrcUsername { get; set; }
        public string TgtUsername { get; set; }
        public string Content { get; set; }
    }
}