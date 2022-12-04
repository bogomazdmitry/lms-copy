using Microsoft.Build.Framework;

namespace LmsCopy.Web.Entites;

public class Subject
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}
