namespace FootballPools.Data.WorldCup;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Group> Groups { get; set; }
    public List<Participant> Participants { get; set; }
}