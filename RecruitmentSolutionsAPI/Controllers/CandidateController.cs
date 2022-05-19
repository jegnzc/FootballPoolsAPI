using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSolutionsAPI.Data;
using RecruitmentSolutionsAPI.ExceptionHandlers;
using RecruitmentSolutionsAPI.Interfaces;
using RecruitmentSolutionsAPI.Models;
using RecruitmentSolutionsAPI.Models.Candidate;

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
            return new CandidateResponse() { FirstName = "Exito" };
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
            var candidateModel = new CandidateResponse();

            var candidate = unitOfWork.Candidate.GetById(id);
            if (candidate == null)
            {
                throw new HttpResponseException(StatusCodes.Status404NotFound, "A: Candidato no encontrado.");
            }
            candidateModel = new CandidateResponse
            {
                Address = candidate.Address,
                Email = candidate.Email,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Phone = candidate.Phone
            };

            return candidateModel;
            //try{}catch{return new CandidateRequest(){Error = new HttpResponseException(StatusCodes.Status404NotFound, e)};}
        }

        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment(
            [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() =>
            Problem();
    }
}