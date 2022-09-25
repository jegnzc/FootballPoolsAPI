using System.Security.Claims;
using FootballPools.Data;
using FootballPools.Data.Context;
using FootballPools.Models.Candidate;
using FootballPools.Models.Company;
using FootballPools.Models.Pipeline;
using FootballPools.Models.Questionnaire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }

        public CompanyController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Send(string toAddress)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var subject = "Hola";
            var body = $"Su id es: {userId}";
            await _emailSender.SendEmailAsync(toAddress, subject, body);
            return Ok();
        }

        [HttpGet]
        [Route("/companies")]
        public async Task<IEnumerable<CompanyResponse>> Get()
        {
            await Send("daskdjasdopkas@gmail.com");

            var companies = _context.Companies.ToList();
            return companies.Select(candidate => new CompanyResponse
            {
                Id = candidate.Id,
                Name = candidate.Name
            })
                .ToList();
        }

        [HttpGet]
        [Route("/companies")]
        public async Task<IActionResult> Post(Register request)
        {
            var companies = _context.Leagues.Add(new League()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = request.LastName
            });
            _context.SaveChanges();

            return Ok();
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