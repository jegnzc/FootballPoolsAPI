using FootballPools.Data.Leagues;
using FootballPools.Data.WorldCup;
using Microsoft.EntityFrameworkCore;

namespace FootballPools.Data;

public interface IApplicationDbContext
{
    public DbSet<League> Leagues { get; set; }
    public DbSet<LeagueInvitation> LeagueInvitations { get; set; }
    public DbSet<LeagueMember> LeagueMembers { get; set; }
    public DbSet<LeagueMemberPrediction> LeagueMemberPredictions { get; set; }
    public DbSet<LeagueType> LeagueTypes { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Match> Matchs { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}