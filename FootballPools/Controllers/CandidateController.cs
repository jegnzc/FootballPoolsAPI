using FootballPools.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        #region borrar

        //private readonly ILogger<CandidateController> _logger;

        //public CandidateController(ILogger<CandidateController> logger)
        //{
        //    _logger = logger;
        //}

        #endregion borrar

        private readonly ApplicationDbContext _context;

        public CandidateController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}