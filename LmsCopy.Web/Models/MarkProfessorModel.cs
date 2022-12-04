using Microsoft.Build.Framework;

namespace LmsCopy.Web.Models;

public class MarkProfessorModel
{
    public Guid Id { get; set; }
    
    [Required]
    public int Value { get; set; }

    [Required]
    public virtual string StudentName { get; set; }

    [Required]
    public virtual string SubjectName { get; set; }
}
