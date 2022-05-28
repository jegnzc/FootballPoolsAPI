using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.Data.Context;
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

        private readonly ApplicationDbContext _context;

        public CandidateController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<CandidateResponse> Post(CandidateRequest request)
        {
            var candidate = new Candidate
            {
                Address = request.Address,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone
            };
            await _context.Candidates.AddAsync(candidate).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return new CandidateResponse();
        }

        [HttpGet]
        public IEnumerable<CandidateResponse> Get()
        {
            var candidates = _context.Candidates.ToList();

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
            var candidate = _context.Candidates.SingleOrDefault(x => x.Id == id);
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
            var candidate = _context.Candidates.FirstOrDefault(x => x.Id == id);
            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Phone = request.Phone;
            candidate.Address = request.Address;
            candidate.Email = request.Email;
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
            return new ApiOkResponse();
        }
    }
}