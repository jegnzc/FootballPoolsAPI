namespace FootballPools.Data
{
    public class LeagueInvitation
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}