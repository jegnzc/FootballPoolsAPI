using FootballPools.Models.Responses;

namespace FootballPools.Models.Position;

public class CreatePositionRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public double Salary { get; set; }
    public bool Status { get; set; }
}

public class CreatePositionResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public double Salary { get; set; }
    public bool Status { get; set; }
}

public class GetAllPositionsResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public double Salary { get; set; }
    public bool Status { get; set; }
}