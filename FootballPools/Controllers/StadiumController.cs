using FootballPools.Data;
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
    public class StadiumController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        public IEmailSender _emailSender { get; set; }
        private readonly UserManager<User> _userManager;

        public StadiumController(IApplicationDbContext context, IEmailSender emailSender, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<List<Stadium>> Get()
        {
            return await _context.Stadiums.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Stadium> Get(int id)
        {
            return await _context.Stadiums.SingleOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<Stadium> Post(CreateStadium request)
        {
            var newStadium = request.Adapt<Stadium>();
            await _context.Stadiums.AddAsync(newStadium);
            await _context.SaveChangesAsync();
            return newStadium;
        }

        [HttpPatch]
        public async Task<Stadium> Post(UpdateStadium request)
        {
            var stadium = _context.Stadiums.SingleOrDefault(x => x.Id == request.Id);
            request.Adapt(stadium);
            _context.Stadiums.Update(stadium);
            await _context.SaveChangesAsync();
            return stadium;
        }
    }
}