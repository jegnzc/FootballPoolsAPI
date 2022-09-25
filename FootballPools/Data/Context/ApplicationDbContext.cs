using Microsoft.EntityFrameworkCore;

namespace FootballPools.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<Pipeline> Pipelines { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<League> Leagues { get; set; }
}