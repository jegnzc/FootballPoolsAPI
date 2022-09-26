using FootballPools.Data.Leagues;
using FootballPools.Data.WorldCup;

namespace FootballPools.Models.WorldCup;

public class CreateTournament
{
    public string Name { get; set; }
}

public class CreateTournamentReponse
{
}

public class UpdateTournament
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdateTournamentResponse
{
}

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

public class CreateGroup
{
    public string Name { get; set; }
    public int TournamentId { get; set; }
}

public class CreateGroupResponse
{
}

public class CreateTournament
{
    public string Name { get; set; }
}

public class CreateTournamentResponse
{
}