using Microsoft.EntityFrameworkCore;

namespace RecruitmentSolutionsAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Candidate> Candidates { get; set; }
}