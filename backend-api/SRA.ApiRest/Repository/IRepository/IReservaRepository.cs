using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.Repository.IRepository
{
    public interface IReservaRepository : IRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> GetReservasPorProfesorAsync(string userId);
        Task<int?> ObtenerProfesorIdDesdeAppUserId(string appUserId);
    }
}