﻿using RecruitmentSolutionsAPI.ExceptionHandlers;

namespace RecruitmentSolutionsAPI.Models.Candidate;

public class CandidateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}

public class CandidateResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    //public GenericException Error { get; set; }
    public ApiResponse Error { get; set; }
}