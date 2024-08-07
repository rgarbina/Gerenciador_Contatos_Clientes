using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Gerenciador_Contatos_Clientes_Back.Data.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected GerenciadorContatosContext context = new GerenciadorContatosContext();

        public void Add(TEntity obj)
        {
            context.Set<TEntity>().Add(obj);
        }

        public async Task AddAsync(TEntity obj)
        {
            await context.Set<TEntity>().AddAsync(obj);
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity obj)
        {
            context.Set<TEntity>().Remove(obj);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(TEntity obj)
        {
            context.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return context.Set<TEntity>().Where(expression);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await context.Set<TEntity>().AnyAsync(expression);
        }
    }
}
