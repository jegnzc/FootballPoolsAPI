using FootballPools.Data.WorldCup;

namespace FootballPools.Data.Leagues;

public class LeagueMemberPrediction
{
    public int Id { get; set; }
    public int FirstParticipantScore { get; set; }
    public int SecondParticipantScore { get; set; }
    public int MatchId { get; set; }
    public Match Match { get; set; }
    public int LeagueMemberId { get; set; }
    public LeagueMember LeagueMember { get; set; }
}