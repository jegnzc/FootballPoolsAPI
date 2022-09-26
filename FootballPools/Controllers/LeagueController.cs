using System.Security.Claims;
using FootballPools.Data.Context;
using FootballPools.Data.Identity;
using FootballPools.Data.Leagues;
using FootballPools.Models.Candidate;
using FootballPools.Models.ExceptionHandlers;
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
    public class LeagueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public LeagueController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpPost("members")]
        public async Task<GetMembersByLeagueIdResponse> Post(GetMembersByLeagueId request)
        {
            return new GetMembersByLeagueIdResponse()
            {
                Members = await _context.LeagueMembers.ToListAsync()
            };
        }

        [HttpPost("member/accept")]
        public async Task<AcceptMemberResponse> Post(AcceptMember request)
        {
            var member = await _context.LeagueMembers.SingleOrDefaultAsync(x => x.UserId == request.Id);
            member.Authorized = true;
            _context.Update(member);
            _context.SaveChangesAsync();
            return new AcceptMemberResponse()
            {
                Member = member
            };
        }

        [HttpGet]
        public async Task<GetLeaguesResponse> Get()
        {
            return new GetLeaguesResponse()
            {
                Leagues = await _context.Leagues.ToListAsync()
            };
        }

        [HttpGet("{id}")]
        public async Task<GetLeagueByIdResponse> Get(int id)
        {
            return new GetLeagueByIdResponse()
            {
                League = await _context.Leagues.SingleOrDefaultAsync(x => x.Id == id)
            };
        }

        [HttpPost]
        [HttpGet("join")]
        public async Task<JoinResponse> Post(Join request)
        {
            var league = await _context.Leagues.SingleOrDefaultAsync(x => x.Id == request.Id);
            _context.LeagueMembers.AddAsync(new LeagueMember()
            {
                LeagueId = request.Id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            });
            await _context.SaveChangesAsync();
            return new JoinResponse()
            {
                League = league
            };
        }

        [HttpPost("create")]
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

        [HttpPost("invite")]
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

        [HttpGet("accept/{token}")]
        public async Task<IActionResult> Get(string token)
        {
            var leagueInvitation = _context.LeagueInvitations.SingleOrDefault(x => x.Token == token);
            if (leagueInvitation == null)
                throw new HttpResponseException(500, "Token inválido");
            _context.LeagueMembers.Add(new LeagueMember()
            {
                LeagueId = leagueInvitation.LeagueId,
                UserId = leagueInvitation.UserId,
                Authorized = true
            });

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}