using FootballPools.Data;
using FootballPools.Data.Context;
using FootballPools.Models.Position;
using Microsoft.AspNetCore.Mvc;

namespace FootballPools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Position Post(CreatePositionRequest request)
        {
            var position = new Position
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Salary = request.Salary,
                Status = request.Status
            };
            _context.Positions.Add(position);
            _context.SaveChanges();
            return position;
        }

        [HttpGet]
        public IEnumerable<Position> GetAll()
        {
            var positions = _context.Positions.ToList();
            return positions;
        }

        [HttpGet("{id}")]
        public Position GetById(int id)
        {
            var positions = _context.Positions.SingleOrDefault(x => x.Id == id);
            return positions;
        }

        [HttpPut("{id}/state")]
        public Position UpdateStatus(int id)
        {
            var positions = _context.Positions.SingleOrDefault(x => x.Id == id);
            positions.Status = !positions.Status;
            _context.Update(positions);
            _context.SaveChanges();
            return positions;
        }

        [HttpPut("{id}")]
        public Position Update(int id, CreatePositionRequest request)
        {
            var positions = _context.Positions.SingleOrDefault(x => x.Id == id);
            positions.Description = request.Description;
            positions.Location = request.Location;
            positions.Salary = request.Salary;
            positions.Status = request.Status;
            positions.Name = request.Name;
            _context.Update(positions);
            _context.SaveChanges();
            return positions;
        }
    }
}