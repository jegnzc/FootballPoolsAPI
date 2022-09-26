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
    public class MatchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public MatchController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<List<Match>> Get()
        {
            return await _context.Matchs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Match> Get(int id)
        {
            return await _context.Matchs.SingleOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<Match> Post(CreateMatch request)
        {
            var newMatch = request.Adapt<Match>();
            await _context.AddAsync(newMatch);
            await _context.SaveChangesAsync();
            return newMatch;
        }
    }
}