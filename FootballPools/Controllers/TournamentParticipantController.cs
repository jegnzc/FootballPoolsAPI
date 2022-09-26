using FootballPools.Data.Context;
using FootballPools.Data.Identity;
using FootballPools.Data.Leagues;
using FootballPools.Data.WorldCup;
using FootballPools.Models.WorldCup;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TournamentParticipantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public TournamentParticipantController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<List<LeagueMemberPrediction>> Get()
        {
            return await _context.LeagueMemberPredictions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<LeagueMemberPrediction> Get(int id)
        {
            return await _context.LeagueMemberPredictions.SingleOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<LeagueMemberPrediction> Post(CreatePrediction request)
        {
            var newPrediction = request.Adapt<LeagueMemberPrediction>();
            await _context.AddAsync(newPrediction);
            await _context.SaveChangesAsync();
            return newPrediction;
        }

        [HttpPatch]
        public async Task<LeagueMemberPrediction> Post(UpdatePrediction request)
        {
            var prediction = _context.LeagueMemberPredictions.SingleOrDefault(x => x.Id == request.Id);
            request.Adapt(prediction);
            _context.Update(prediction);
            await _context.SaveChangesAsync();
            return prediction;
        }
    }
}