using FootballPools.Data.Context;
using FootballPools.Data.Identity;
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
    public class TournamentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public TournamentController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<List<Tournament>> Get()
        {
            return await _context.Tournaments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Tournament> Get(int id)
        {
            return await _context.Tournaments.SingleOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<Tournament> Post(CreateTournament request)
        {
            var newTournament = request.Adapt<Tournament>();
            await _context.AddAsync(newTournament);
            await _context.SaveChangesAsync();
            return newTournament;
        }

        [HttpPatch]
        public async Task<Tournament> Post(UpdateTournament request)
        {
            var tournament = _context.Tournaments.SingleOrDefault(x => x.Id == request.Id);
            request.Adapt(tournament);
            _context.Update(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }
    }
}