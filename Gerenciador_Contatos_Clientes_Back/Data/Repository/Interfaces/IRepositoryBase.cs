using System.Linq.Expressions;

namespace Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Dispose();
        void SaveChanges();
        Task SaveChangesAsync();
        IQueryable<TEntity> GetAllAsNoTracking();
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
