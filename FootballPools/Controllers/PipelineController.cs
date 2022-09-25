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
    public class PipelineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PipelineController(ApplicationDbContext context)
        {
            this._context = context;
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
            _context.Pipelines.Add(pipeline);
            _context.SaveChanges();
            return new PipelineResponse();
        }
    }
}