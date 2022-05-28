namespace RecruitmentSolutionsAPI.Data;

public class Questionnaire
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Score { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public List<Question> Questions { get; set; }
}