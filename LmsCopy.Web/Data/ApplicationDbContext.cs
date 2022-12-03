using LmsCopy.Web.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LmsCopy.Web.Data;

public class ApplicationDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Mark>()
            .HasOne<Subject>(m => m.Subject);
        
        
        modelBuilder.Entity<Mark>()
            .HasOne<User>(m => m.Student)
            .WithMany(u => u.StudentMarks);
        
        modelBuilder.Entity<Mark>()
            .HasOne<User>(m => m.Professor)
            .WithMany(u => u.ProfessorMarks);
    }
}
