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
    public class QuestionnaireController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public QuestionnaireController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
            unitOfWork.Questionnaire.Add(questionnaire);
            unitOfWork.Save();
            return new QuestionnaireResponse();
        }
    }
}