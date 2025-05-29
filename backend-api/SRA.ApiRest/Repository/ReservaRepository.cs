using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Data;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.Profesor)
                .Include(r => r.FranjaHoraria)
                .ToListAsync();
        }

        public async Task<Reserva?> GetAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Profesor)
                .Include(r => r.FranjaHoraria)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reservas.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> CreateAsync(Reserva entity)
        {
            await _context.Reservas.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(Reserva entity)
        {
            _context.Reservas.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.Reservas.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache()
        {
        }
    }
}
