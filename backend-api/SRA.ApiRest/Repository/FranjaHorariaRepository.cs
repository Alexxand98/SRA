using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Data;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Repository
{
    public class FranjaHorariaRepository : IFranjaHorariaRepository
    {
        private readonly ApplicationDbContext _context;

        public FranjaHorariaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<FranjaHoraria>> GetAllAsync()
        {
            return await _context.FranjasHorarias.ToListAsync();
        }

        public async Task<FranjaHoraria?> GetAsync(int id)
        {
            return await _context.FranjasHorarias.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.FranjasHorarias.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> CreateAsync(FranjaHoraria entity)
        {
            await _context.FranjasHorarias.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(FranjaHoraria entity)
        {
            _context.FranjasHorarias.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.FranjasHorarias.Remove(entity);
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
