using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Interfaces;
using RecruitmentSolutionsAPI.Models;
using RecruitmentSolutionsAPI.Models.Candidate;
using RecruitmentSolutionsAPI.Models.ExceptionHandlers;
using RecruitmentSolutionsAPI.Models.Responses;

namespace RecruitmentSolutionsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        #region borrar

        //private readonly ILogger<CandidateController> _logger;

        //public CandidateController(ILogger<CandidateController> logger)
        //{
        //    _logger = logger;
        //}

        #endregion borrar

        private readonly IUnitOfWork unitOfWork;

        public CandidateController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public CandidateResponse Post(CandidateRequest request)
        {
            var candidate = new Candidate
            {
                Address = request.Address,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone
            };
            unitOfWork.Candidate.Add(candidate);
            unitOfWork.Save();
            return new CandidateResponse();
        }

        [HttpGet]
        public IEnumerable<CandidateResponse> Get()
        {
            var candidates = unitOfWork.Candidate.GetAll();

            return candidates.Select(candidate => new CandidateResponse
            {
                Address = candidate.Address,
                Email = candidate.Email,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Phone = candidate.Phone
            })
                .ToList();
        }

        [HttpGet("{id}")]
        public CandidateResponse GetById(int id)
        {
            var candidate = unitOfWork.Candidate.GetById(id);
            var response = new CandidateResponse
            {
                Address = candidate.Address,
                Email = candidate.Email,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Phone = candidate.Phone
            };
            return response;
        }

        [HttpPut("{id}")]
        public ApiOkResponse UpdateCandidate(int id, CandidateRequest request)
        {
            var candidate = unitOfWork.Candidate.GetById(id);
            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Phone = request.Phone;
            candidate.Address = request.Address;
            candidate.Email = request.Email;
            unitOfWork.Candidate.Update(candidate);
            unitOfWork.Save();
            return new ApiOkResponse();
        }
    }
}