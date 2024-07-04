using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace HotelWebApi.Data
{
    public class Item
    {
        public int Id { get; set; }

        
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

    }
}