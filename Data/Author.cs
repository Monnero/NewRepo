using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HotelWebApi.Data
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; } = string.Empty;

        public bool IsMainAuthor { get; set; }

        public List<Book> Books { get; set; } = new();

        [Required]
        public string Language { get; set; } = string.Empty;

        public void Validate()
        {
            if (IsEnglish(LastName) != IsEnglish(FirstName))
            {
                throw new Exception("Имена и фамилия должны быть на одном языке.");
            }
        }

        private static bool IsEnglish(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }
    }
}