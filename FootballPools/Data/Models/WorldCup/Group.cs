namespace FootballPools.Data.WorldCup;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; }
}