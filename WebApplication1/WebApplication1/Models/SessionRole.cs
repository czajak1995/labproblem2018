using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SessionRole
    {
        public string sessionId { get; set; }
        public Role role { get; set; }
    }
}