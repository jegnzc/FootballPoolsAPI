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
    public class WorldCupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public WorldCupController(ApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
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
    }
}