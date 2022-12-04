namespace LmsCopy.Web.Entites;

public class Mark
{
    public Guid Id { get; set; }
    
    public int Value { get; set; }
    
    public Guid StudentId { get; set; }
    
    public virtual User Student { get; set; }
    
    public Guid ProfessorId { get; set; }
    
    public virtual User Professor { get; set; }
    
    public Guid SubjectId { get; set; }
    
    public virtual Subject Subject { get; set; }
}
