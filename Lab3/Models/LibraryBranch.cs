namespace WebApplication1.Models;

public class LibraryBranch
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string ImageUrl { get; set; }
    
    public ICollection<Book> Books { get; set; }
}