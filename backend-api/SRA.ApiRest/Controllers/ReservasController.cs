using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Data;
using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        {
            return await _context.Reservas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> Post(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva);
        }
    }
}
