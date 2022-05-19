namespace RecruitmentSolutionsAPI.Models;

public class GenericException
{
    public string Useful { get; set; }
    public int StatusCode { get; set; }
    public string StackTrace { get; set; }
}