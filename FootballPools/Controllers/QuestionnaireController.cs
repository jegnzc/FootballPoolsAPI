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
    public class QuestionnaireController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionnaireController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public QuestionnaireResponse Post(QuestionnaireRequest request)
        {
            var questionnaire = new Questionnaire
            {
                Name = request.Name,
                CompanyId = request.CompanyId,
                Score = request.Score
            };
            _context.Questionnaires.Add(questionnaire);
            _context.SaveChanges();
            return new QuestionnaireResponse();
        }
    }
}