using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        [Required]
        public int Temp { get; set; }
        public DateTime Date { get; set; }
        public int DeviceId { get; set; }
    }
}