using FootballPools.Data.Leagues;
using FootballPools.Data.WorldCup;

namespace FootballPools.Models.WorldCup;

public class CreateTournamentParticipant
{
    public string Name { get; set; }
    public int TournamentId { get; set; }
}

public class CreateTournamentParticipantResponse
{
}

public class UpdateTournamentParticipant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TournamentId { get; set; }
}

public class UpdateTournamentParticipantResponse
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

public class CreatePrediction
{
    public int FirstParticipantScore { get; set; }
    public int SecondParticipantScore { get; set; }
    public int MatchId { get; set; }
    public int LeagueMemberId { get; set; }
}

public class CreatePredictionResponse
{
}

public class UpdatePrediction
{
    public int Id { get; set; }
    public int FirstParticipantScore { get; set; }
    public int SecondParticipantScore { get; set; }
    public int MatchId { get; set; }
    public int LeagueMemberId { get; set; }
}

public class UpdatePredictionResponse
{
}