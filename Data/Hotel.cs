using HotelWebApi.Data;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
//public class Books
//{
//    public int Id { get; set; }
//    public string Title { get; set; } = string.Empty;
//    public string Description { get; set; } = string.Empty;
//    public List<Author> Authors { get; set; } = new();
//}
//public class Author
//{
//    public int Id { get; set; }
//    public string FirstName { get; set; } = string.Empty;
//    public string LastName { get; set; } = string.Empty;
//    public bool IsMainAuthor { get; set; }
//}