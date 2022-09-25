using FootballPools.Data.Context;
using FootballPools.Models.Pipeline;
using Microsoft.AspNetCore.Mvc;
using FootballPools.Data;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PipelineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PipelineController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public PipelineResponse Post(PipelineRequest request)
        {
            var pipeline = new Pipeline
            {
                Name = request.Name,
                Description = request.Description,
                CompanyId = request.CompanyId
            };
            _context.Pipelines.Add(pipeline);
            _context.SaveChanges();
            return new PipelineResponse();
        }
    }
}