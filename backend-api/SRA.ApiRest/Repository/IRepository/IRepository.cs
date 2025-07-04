﻿namespace SRA.ApiRest.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> Save();
        void ClearCache();
    }
}
