using FootballPools.Data.Context;
using FootballPools.Data.Identity;
using FootballPools.Data.Leagues;
using FootballPools.Models.WorldCup;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public PredictionController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
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
            var member = await _context.LeagueMembers.SingleOrDefaultAsync(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var prediction = request.Adapt<LeagueMemberPrediction>();
            prediction.LeagueMemberId = member?.Id ?? 0;
            await _context.AddAsync(prediction);
            await _context.SaveChangesAsync();
            return prediction;
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