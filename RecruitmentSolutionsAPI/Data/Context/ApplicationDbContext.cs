using Microsoft.EntityFrameworkCore;

namespace RecruitmentSolutionsAPI.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
}