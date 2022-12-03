using Microsoft.AspNetCore.Identity;

namespace LmsCopy.Web.Entites;

public class User : IdentityUser<Guid>
{
    public IEnumerable<Mark> StudentMarks { get; set; } = new List<Mark>();

    public IEnumerable<Mark> ProfessorMarks { get; set; } = new List<Mark>();
}
