using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SrcId { get; set; }
        public int TgtId { get; set; }
        public string SrcUsername { get; set; }
        public string TgtUsername { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}