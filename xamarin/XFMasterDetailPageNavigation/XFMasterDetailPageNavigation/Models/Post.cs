using System;
using System.Collections.Generic;
using System.Text;

namespace XFMasterDetailPageNavigation.Models
{
    public class Post
    {
        public string Nick { get; set; }
        public string Message { get; set; }

        public string FullMessage
        {
            get { return Nick + ": " + Message;  }
        }
    }
}
