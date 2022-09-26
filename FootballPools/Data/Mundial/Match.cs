namespace FootballPools.Data.Mundial;

public class Match
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? FirstParticipantId { get; set; }
    public Participant? FirstParticipant { get; set; }
    public int? SecondParticipantId { get; set; }
    public Participant? SecondParticipant { get; set; }
    public int? WinnerId { get; set; }
    public Participant? Winner { get; set; }
    public int? FirstParticipantScore { get; set; }
    public int? SecondParticipantScore { get; set; }
    public DateTime Schedule { get; set; }
    public int StadiumId { get; set; }
    public Stadium Stadium { get; set; }
}