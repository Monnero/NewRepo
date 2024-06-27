using System.ComponentModel.DataAnnotations;

namespace HotelWebApi.Data
{
    public class AuthorBook
    {
        [Key]
        public int AuthorsId { get; set; }
        [Key]
        public int BooksId { get; set; }
        public virtual Author Author { get; set; } = new Author();
        public virtual Book Book { get; set; } = new Book();

    }
}
