using FootballPools.Data.Context;
using FootballPools.Models.Questionnaire;
using Microsoft.AspNetCore.Mvc;
using FootballPools.Data;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionnaireController(ApplicationDbContext context)
        {
            _context = context;
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