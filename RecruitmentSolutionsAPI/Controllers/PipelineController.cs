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
    public class PipelineController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PipelineController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public PipelineResponse Post(PipelineRequest request)
        {
            var pipeline = new Pipeline
            {
                Name = request.Name,
                Description = request.Description,
                CompanyId = request.CompanyId
            };
            unitOfWork.Pipeline.Add(pipeline);
            unitOfWork.Save();
            return new PipelineResponse();
        }
    }
}