namespace FootballPools.Data;

public class Pipeline
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}