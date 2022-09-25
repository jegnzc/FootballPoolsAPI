using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Models.Candidate;

public class CompanyRequest
{
    public string Name { get; set; }
}

public class CompanyResponse
{
    public int? Id { get; set; }
    public string Name { get; set; }
}