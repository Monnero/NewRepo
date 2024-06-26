using System.ComponentModel.DataAnnotations;

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

        public List<Author> Authors { get; set; } = new();
    }

}
