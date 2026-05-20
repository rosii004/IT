namespace WebApplication1.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    
    public int LibraryBranchId { get; set; }
    public LibraryBranch LibraryBranch { get; set; }
    
    public ICollection<Member> Members { get; set; }
}