namespace FootballPools.Data.Leagues;

public class League
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public int LeagueTypeId { get; set; }
    public double ParticipationPrice { get; set; }
    public LeagueType LeagueType { get; set; }
    public List<LeagueInvitation> LeagueInvitations { get; set; }
    public List<LeagueMember> LeagueMembers { get; set; }
}