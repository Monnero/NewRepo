using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelWebApi.Data
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        //[JsonIgnore]
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    }

}
