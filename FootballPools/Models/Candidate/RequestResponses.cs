﻿using FootballPools.Data.Identity;
using FootballPools.Data.Leagues;
using FootballPools.Models.Responses;
using Microsoft.AspNetCore.Components;

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

public class GetLeagues
{
}

public class GetLeaguesResponse
{
    public List<League> Leagues { get; set; }
}

public class GetLeagueById
{
    public int Id { get; set; }
}

public class GetLeagueByIdResponse
{
    public League League { get; set; }
}

public class Join
{
    public int Id { get; set; }
}

public class JoinResponse
{
    public League League { get; set; }
}

public class GetMembersByLeagueId
{
    public int Id { get; set; }
}

public class GetMembersByLeagueIdResponse
{
    public List<LeagueMember> Members { get; set; }
}

public class AcceptMember
{
    public string Id { get; set; }
}

public class AcceptMemberResponse
{
    public LeagueMember Member { get; set; }
}