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
            throw new
                HttpResponseException(StatusCodes.Status404NotFound, "E002",
                    "Candidato inválido", request);
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
            return new CandidateResponse() { FirstName = "Exito" };
        }

        [HttpGet]
        public IEnumerable<CandidateResponse> Get()
        {
            throw new HttpResponseException(StatusCodes.Status404NotFound, "E001", "Candidato no encontrado.");
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
        public ApiResponse GetById(int id)
        {
            var candidate = unitOfWork.Candidate.GetById(id);
            //if (candidate == null)
            //{
            //    throw new HttpResponseException(StatusCodes.Status404NotFound, "E001", "Candidato no encontrado.");
            //}
            var response = new CandidateResponse
            {
                Address = candidate.Address,
                Email = candidate.Email,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Phone = candidate.Phone
            };
            return new ApiOkResponse(response);
        }
    }
}