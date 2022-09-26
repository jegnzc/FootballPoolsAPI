using FootballPools.Data.Leagues;
using FootballPools.Data.WorldCup;

namespace FootballPools.Models.WorldCup;

public class CreateMatch
{
    public string Name { get; set; }
    public int? FirstParticipantId { get; set; }
    public int? SecondParticipantId { get; set; }
    public int? WinnerId { get; set; }
    public DateTime Schedule { get; set; }
    public int StadiumId { get; set; }
}

public class CreateMatchResponse
{
}

public class UpdateMatch
{
    public int Id { get; set; }
    public int FirstParticipantScore { get; set; }
    public int SecondParticipantScore { get; set; }
}

public class UpdateMatchResponse
{
}