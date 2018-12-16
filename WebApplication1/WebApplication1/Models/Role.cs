using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ManageUsers { get; set; }
        public bool AverageTemperatures { get; set; }
        public bool YearTemperatures { get; set; }
        public bool CanExport { get; set; }
        public bool UseMessanger { get; set; }

    }
}