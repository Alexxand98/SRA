using SRA.ApiRest.Data;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace SRA.ApiRest.Repository
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfesorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Profesor>> GetAllAsync()
        {
            return await _context.Profesores.ToListAsync();
        }

        public async Task<Profesor?> GetAsync(int id)
        {
            return await _context.Profesores.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Profesores.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> CreateAsync(Profesor entity)
        {
            await _context.Profesores.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(Profesor entity)
        {
            _context.Profesores.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.Profesores.Remove(entity);
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
