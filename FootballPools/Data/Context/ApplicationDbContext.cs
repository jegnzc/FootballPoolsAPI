using FootballPools.Data.Leagues;
using FootballPools.Data.WorldCup;
using Microsoft.EntityFrameworkCore;

namespace FootballPools.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<League> Leagues { get; set; }
    public DbSet<LeagueInvitation> LeagueInvitations { get; set; }
    public DbSet<LeagueMember> LeagueMembers { get; set; }
    public DbSet<LeagueMemberPrediction> LeagueMemberPredictions { get; set; }
    public DbSet<LeagueType> LeagueTypes { get; set; }

    //
    public DbSet<Group> Groups { get; set; }

    public DbSet<Match> Matchs { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
}