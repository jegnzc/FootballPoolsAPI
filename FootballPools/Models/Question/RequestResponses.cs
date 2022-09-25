using FootballPools.Models.Responses;

namespace FootballPools.Models.Question;

public class QuestionRequest
{
    public string Name { get; set; }
    public double Value { get; set; }
    public int QuestionnaireId { get; set; }
}

public class QuestionResponse
{
    public string Name { get; set; }
    public double Value { get; set; }
    public int QuestionnaireId { get; set; }
}