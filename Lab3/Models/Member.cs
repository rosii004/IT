using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Member
{
    public int  Id { get; set; }
    
    [Required]
    public string FullName {  get; set; }
    
    [Range (10000, 99999, ErrorMessage = "Потребни се 5 цифри")]
    [Display(Name = "Внеси код")]
    public int MembersCode { get; set; }
    
    public ICollection<Book> Books { get; set; }
}