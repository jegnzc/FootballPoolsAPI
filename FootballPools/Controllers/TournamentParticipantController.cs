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
        public async Task<List<Participant>> Get()
        {
            return await _context.Participants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Participant> Get(int id)
        {
            return await _context.Participants.SingleOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<Participant> Post(CreateTournamentParticipant request)
        {
            var newParticipant = request.Adapt<Participant>();
            await _context.AddAsync(newParticipant);
            await _context.SaveChangesAsync();
            return newParticipant;
        }

        [HttpPatch]
        public async Task<Participant> Post(UpdateTournamentParticipant request)
        {
            var participant = _context.Participants.SingleOrDefault(x => x.Id == request.Id);
            request.Adapt(participant);
            _context.Update(participant);
            await _context.SaveChangesAsync();
            return participant;
        }
    }
}