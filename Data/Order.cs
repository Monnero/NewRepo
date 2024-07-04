using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelWebApi.Data
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        
        public List<Item> Items { get; set; } = new List<Item>();
    }

}
