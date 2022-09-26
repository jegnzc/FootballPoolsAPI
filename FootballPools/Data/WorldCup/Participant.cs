namespace FootballPools.Data.WorldCup;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; }
}