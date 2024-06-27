using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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

        public virtual List<Book> Books { get; set; } = new List<Book>();


        public bool IsValidate()
        {
            if ((IsEnglish(LastName) == true && IsEnglish(FirstName) == true) || (IsRussian(LastName) == true && IsRussian(FirstName) == true))
            {
                return true;
            }
            else return false;
        }

        private static bool IsEnglish(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }
        private static bool IsRussian(string text)
        {
            return Regex.IsMatch(text, @"^[а-яА-ЯёЁ]+$");
        }
    }
}