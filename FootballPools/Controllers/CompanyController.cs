using System.Security.Claims;
using FootballPools.Data;
using FootballPools.Data.Context;
using FootballPools.Models.Candidate;
using FootballPools.Models.Company;
using FootballPools.Models.ExceptionHandlers;
using FootballPools.Models.Pipeline;
using FootballPools.Models.Questionnaire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public CompanyController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpPost("createLeague")]
        public async Task<IActionResult> Post(CreateLeague request)
        {
            await _context.Leagues.AddAsync(new League()
            {
                Name = request.Name,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("createLeagueInvitation")]
        public async Task<IActionResult> Post(CreateLeagueInvitation request)
        {
            var leagueInvitation = new LeagueInvitation()
            {
                LeagueId = request.LeagueId,
                UserId = request.UserId,
                Token = Guid.NewGuid().ToString(),
            };
            await _context.LeagueInvitations.AddAsync(new LeagueInvitation()
            {
                LeagueId = request.LeagueId,
                UserId = request.UserId,
                Token = leagueInvitation.Token,
            });
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(request.UserId);
            var subject = "Invitación a liga";
            var body = "https://localhost:7200/company/acceptLeagueInvitation/" + leagueInvitation.Token;

            await _emailSender.SendEmailAsync(user.Email, subject, body);

            return Ok();
        }

        [HttpGet("acceptLeagueInvitation/{leagueInvitationToken}")]
        public async Task<IActionResult> Get(string leagueInvitationToken)
        {
            var leagueInvitation = _context.LeagueInvitations.SingleOrDefault(x => x.Token == leagueInvitationToken);
            if (leagueInvitation == null)
                throw new HttpResponseException(500, "Token inválido");
            _context.LeagueMembers.Add(new LeagueMember()
            {
                LeagueId = leagueInvitation.LeagueId,
                UserId = leagueInvitation.UserId
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("/companies")]
        public async Task<IEnumerable<CompanyResponse>> Get()
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