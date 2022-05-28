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
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public CandidateResponse Post(CompanyRequest request)
        {
            var company = new Company
            {
                Name = request.Name
            };
            unitOfWork.Company.Add(company);
            unitOfWork.Save();
            return new CandidateResponse();
        }

        [HttpGet]
        [Route("/companies")]
        public IEnumerable<CompanyResponse> Get()
        {
            var companies = unitOfWork.Company.GetAll();

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
            var candidate = unitOfWork.Company.GetById(id);
            var response = new CompanyResponse
            {
                Name = candidate.Name
            };
            return response;
        }

        [HttpGet("{id}/pipelines")]
        public IEnumerable<PipelineResponse> GetCompanyPipelineById(int id)
        {
            var pipelines = unitOfWork.Company.GetAllPopulated(x => x.Pipelines);
            var pipelinesResponses = new List<PipelineResponse>();
            //foreach (var pipeline in pipelines)
            //{
            //    var pipelineResponse = new PipelineResponse
            //    {
            //        Name = pipeline.Name,
            //        Description = pipeline.Description,
            //        CompanyId = pipeline.CompanyId
            //    };
            //    pipelinesResponses.Add(pipelineResponse);
            //}
            return pipelinesResponses;
        }

        [HttpGet("{id}/pipeline/{pipelineId}")]
        public PipelineResponse GetCompanyPipelineById(int id, int pipelineId)
        {
            var pipeline = unitOfWork.Company.GetById(id).Pipelines.FirstOrDefault(x => x.Id == pipelineId);
            var response = new PipelineResponse
            {
                Name = pipeline.Name
            };
            return response;
        }

        [HttpGet("{id}/questionnaires")]
        public IEnumerable<QuestionnaireResponse> GetCompanyQuestionnaireById(int id)
        {
            var questionnaires = unitOfWork.Company.GetById(id).Questionnaires.ToList();
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
            var questionnaire = unitOfWork.Company.GetById(id).Questionnaires.FirstOrDefault(x => x.Id == questionnaireId);
            var response = new QuestionnaireResponse
            {
                Name = questionnaire.Name
            };
            return response;
        }
    }
}