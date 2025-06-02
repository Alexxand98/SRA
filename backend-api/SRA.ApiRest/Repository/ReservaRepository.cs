using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Data;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;
using static SRA.ApiRest.Models.Entity.Reserva;

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

        public async Task<IEnumerable<Reserva>> GetReservasPorProfesorAsync(string userId)
        {
            return await _context.Reservas
                .Include(r => r.FranjaHoraria)
                .Include(r => r.Profesor)
                .Where(r => r.Profesor.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<int?> ObtenerProfesorIdDesdeAppUserId(string appUserId)
        {
            var profesor = await _context.Profesores.FirstOrDefaultAsync(p => p.AppUserId == appUserId);
            return profesor?.Id;
        }

        public async Task<(bool EsValida, List<string> Errores)> ValidarReservaAsync(Reserva reserva)
        {
            var errores = new List<string>();

            bool esNoLectivo = await _context.DiasNoLectivos.AnyAsync(d => d.Fecha.Date == reserva.Fecha.Date);
            if (esNoLectivo)
                errores.Add("No se puede reservar en un día no lectivo.");

            bool franjaOcupada = await _context.Reservas.AnyAsync(r =>
                r.Fecha.Date == reserva.Fecha.Date &&
                r.FranjaHorariaId == reserva.FranjaHorariaId);
            if (franjaOcupada)
                errores.Add("La franja horaria ya está ocupada para esa fecha.");

            bool duplicado = await _context.Reservas.AnyAsync(r =>
                r.Fecha.Date == reserva.Fecha.Date &&
                r.FranjaHorariaId == reserva.FranjaHorariaId &&
                r.ProfesorId == reserva.ProfesorId);
            if (duplicado)
                errores.Add("El profesor ya tiene una reserva en esa franja horaria.");

            return (errores.Count == 0, errores);
        }

        public async Task<IEnumerable<Reserva>> GetReservasPendientesAsync()
        {
            return await _context.Reservas
                .Include(r => r.Profesor)
                .Include(r => r.FranjaHoraria)
                .Where(r => r.Estado == EstadoReserva.Pendiente)
                .ToListAsync();
        }

        public async Task<bool> ActualizarEstadoAsync(int id, string nuevoEstado)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            if (!Enum.TryParse<EstadoReserva>(nuevoEstado, out var estadoEnum))
                return false;

            reserva.Estado = estadoEnum;
            return await Save();
        }


        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache() { }
    }
}
