using FootballPools.Models.Responses;

namespace FootballPools.Models.Pipeline;

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
    public int? CompanyId { get; set; }
}