using EMS.Domain;

namespace EMS.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public  DbSet<Employee> Employees { get; set; }
    public  DbSet<PersonalDetails> PersonalDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.PersonalDetails)
            .WithOne(p => p.Employee)
            .HasForeignKey<PersonalDetails>(p => p.EmployeeId);
    }
}