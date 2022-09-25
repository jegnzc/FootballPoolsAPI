namespace FootballPools.Data
{
    public class League
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<LeagueInvitation> LeagueInvitations { get; set; }
        public List<LeagueMember> LeagueMembers { get; set; }
    }
}