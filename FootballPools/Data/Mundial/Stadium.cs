namespace FootballPools.Data.Mundial;

public class Stadium
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Match> Matches { get; set; }
}