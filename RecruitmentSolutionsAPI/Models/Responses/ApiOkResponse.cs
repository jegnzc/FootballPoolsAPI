namespace RecruitmentSolutionsAPI.Models.Responses;

public class ApiOkResponse
{
    public object? Result { get; }

    public ApiOkResponse(object? result = null)
    {
        Result = result;
    }
}