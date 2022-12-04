using Microsoft.Build.Framework;

namespace LmsCopy.Web.Models;

public class MarkStudentModel
{
    public Guid Id { get; set; }
    
    [Required]
    public int Value { get; set; }

    [Required]
    public virtual string ProfessorName { get; set; }

    [Required]
    public virtual string SubjectName { get; set; }
}
