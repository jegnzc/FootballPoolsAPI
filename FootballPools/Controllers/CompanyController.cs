using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("/companies")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IEnumerable<CompanyResponse> Get()
        {
            var companies = _context.Companies.ToList();

            return companies.Select(candidate => new CompanyResponse
            {
                Id = candidate.Id,
                Name = candidate.Name
            })
                .ToList();
        }

        [HttpGet("{id}")]
        public CompanyResponse GetById(int id)
        {
            var candidate = _context.Companies.SingleOrDefault(x => x.Id == id);
            var response = new CompanyResponse
            {
                Name = candidate.Name
            };
            return response;
        }

        [HttpGet("{id}/pipelines")]
        public IEnumerable<PipelineResponse> GetCompanyPipelineById(int id)
        {
            var pipelines = _context.Companies.Where(x => x.Id == id).SelectMany(x => x.Pipelines).ToList();
            var pipelinesResponses = new List<PipelineResponse>();
            foreach (var pipeline in pipelines)
            {
                var pipelineResponse = new PipelineResponse
                {
                    Name = pipeline.Name,
                    Description = pipeline.Description,
                    CompanyId = pipeline.CompanyId
                };
                pipelinesResponses.Add(pipelineResponse);
            }
            return pipelinesResponses;
        }

        [HttpGet("{id}/pipeline/{pipelineId}")]
        public PipelineResponse GetCompanyPipelineById(int id, int pipelineId)
        {
            var pipeline = _context.Companies
                .Where(x => x.Id == id)
                .SelectMany(x => x.Pipelines)
                .ToList()
                .SingleOrDefault(x => x.Id == pipelineId);
            var response = new PipelineResponse
            {
                Name = pipeline.Name,
                Description = pipeline.Description
            };
            return response;
        }

        [HttpGet("{id}/questionnaires")]
        public IEnumerable<QuestionnaireResponse> GetCompanyQuestionnaireById(int id)
        {
            var questionnaires = _context.Companies.Where(x => x.Id == id).SelectMany(x => x.Questionnaires).ToList();
            var questionnaireResponses = new List<QuestionnaireResponse>();
            foreach (var questionnaire in questionnaires)
            {
                var questionnaireResponse = new QuestionnaireResponse
                {
                    Name = questionnaire.Name,
                    CompanyId = questionnaire.CompanyId,
                    Score = questionnaire.Score
                };
                questionnaireResponses.Add(questionnaireResponse);
            }
            return questionnaireResponses;
        }

        [HttpGet("{id}/questionnaire/{questionnaireId}")]
        public QuestionnaireResponse GetCompanyQuestionnaireById(int id, int questionnaireId)
        {
            var questionnaire = _context.Companies
                .Where(x => x.Id == id)
                .SelectMany(x => x.Questionnaires)
                .ToList()
                .SingleOrDefault(x => x.Id == questionnaireId);
            var response = new QuestionnaireResponse
            {
                Name = questionnaire.Name,
                Score = questionnaire.Score
            };
            return response;
        }
    }
}