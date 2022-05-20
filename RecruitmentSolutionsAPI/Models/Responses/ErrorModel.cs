namespace RecruitmentSolutionsAPI.Models.Responses;

public class ErrorModel
{
    public string Type { get; set; }
    public string StackTrace { get; set; }
    public string PublicMessage { get; set; }
    public string InternalCode { get; set; }
    public string OriginalErrorMessage { get; set; }
    public string TargetSite { get; set; }
    public object Request { get; set; }
}