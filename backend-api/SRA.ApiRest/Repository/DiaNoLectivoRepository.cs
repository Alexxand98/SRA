using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Data;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Repository
{
    public class DiaNoLectivoRepository : IDiaNoLectivoRepository
    {
        private readonly ApplicationDbContext _context;

        public DiaNoLectivoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<DiaNoLectivo>> GetAllAsync()
        {
            return await _context.DiasNoLectivos.ToListAsync();
        }

        public async Task<DiaNoLectivo?> GetAsync(int id)
        {
            return await _context.DiasNoLectivos.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.DiasNoLectivos.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> CreateAsync(DiaNoLectivo entity)
        {
            await _context.DiasNoLectivos.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(DiaNoLectivo entity)
        {
            _context.DiasNoLectivos.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.DiasNoLectivos.Remove(entity);
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
