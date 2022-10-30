using FootballPools.Data;
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
        private readonly IApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public TournamentController(IApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        public TournamentController(IApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Tournaments.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tournament = _context.Tournaments.SingleOrDefault(x => x.Id == id);
            if (tournament == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateTournament request)
        {
            if (request.Name == null)
                return BadRequest();
            var newTournament = request.Adapt<Tournament>();
            _context.Tournaments.Add(newTournament);
            _context.SaveChangesAsync();
            return Ok(newTournament);
        }

        [HttpPatch]
        public IActionResult Patch(UpdateTournament request)
        {
            var tournament = _context.Tournaments.SingleOrDefault(x => x.Id == request.Id);
            request.Adapt(tournament);
            _context.Tournaments.Update(tournament);
            _context.SaveChangesAsync();
            return Ok(tournament);
        }
    }
}