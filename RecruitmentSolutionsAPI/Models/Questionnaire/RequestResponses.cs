using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Models.Candidate;

public class QuestionnaireRequest
{
    public string Name { get; set; }
    public double Score { get; set; }
    public int CompanyId { get; set; }
}

public class QuestionnaireResponse
{
    public string Name { get; set; }
    public double Score { get; set; }
    public int CompanyId { get; set; }
}