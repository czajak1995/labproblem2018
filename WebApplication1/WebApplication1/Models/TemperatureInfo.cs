using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TemperatureInfo
    {
        public Temperature Min { get; set; }
        public Temperature Max { get; set; }
        public Device Device { get; set; }
        public int Average { get; set; }
    }
}