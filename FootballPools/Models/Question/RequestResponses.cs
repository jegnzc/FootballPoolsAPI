using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Models.Candidate;

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