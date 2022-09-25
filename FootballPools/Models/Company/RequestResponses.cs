using FootballPools.Models.Responses;

namespace FootballPools.Models.Company;

public class CompanyRequest
{
    public string Name { get; set; }
}

public class CompanyResponse
{
    public int? Id { get; set; }
    public string Name { get; set; }
}