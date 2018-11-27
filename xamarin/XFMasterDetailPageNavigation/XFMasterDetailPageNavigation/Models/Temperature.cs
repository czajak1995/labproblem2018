using System;
using System.Collections.Generic;
using System.Text;

namespace XFMasterDetailPageNavigation.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        
        public int Temp { get; set; }
        public DateTime Date { get; set; }
        public int DeviceId { get; set; }
    }
}
