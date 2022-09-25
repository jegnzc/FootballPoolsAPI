using RecruitmentSolutionsAPI.Areas.Identity.Data;
using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Models.Candidate;

public class Register
{
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
}

public class RegisterResponse
{
    public User User { get; set; }
}

public class Login
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
}