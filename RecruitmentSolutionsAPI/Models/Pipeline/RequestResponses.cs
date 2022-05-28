using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Models.Candidate;

public class PipelineRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
}

public class PipelineResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
}