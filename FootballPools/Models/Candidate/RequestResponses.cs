using FootballPools.Data;
using FootballPools.Models.Responses;

namespace FootballPools.Models.Candidate;

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

public class CreateLeagueInvitation
{
    public int LeagueId { get; set; }
    public string UserId { get; set; }
}

public class CreateLeagueInvitationResponse
{
}

public class CreateLeague
{
    public string Name { get; set; }
}

public class CreateLeagueResponse
{
}