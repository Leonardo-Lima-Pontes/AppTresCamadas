using AppTresCamadas.Business.Interfaces;
using AppTresCamadas.Business.Models.Shared;
using AppTresCamadas.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppTresCamadas.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected readonly AppDbContext Database;
        protected readonly DbSet<TEntity> EntityDbSet;

        protected RepositoryBase(AppDbContext database)
        {
            Database = database;
            EntityDbSet = database.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate) =>
            await EntityDbSet.AsNoTracking().Where(predicate).ToListAsync();

        public virtual async Task<TEntity> ObterPorId(Guid id) => await EntityDbSet.FindAsync(id);

        public virtual async Task<IList<TEntity>> ObterTodos() => await EntityDbSet.ToListAsync();

        public virtual async Task Adicionar(TEntity entity)
        {
            await EntityDbSet.AddAsync(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            EntityDbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            EntityDbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges() => await Database.SaveChangesAsync();

        public async void Dispose() => await Database.DisposeAsync();
    }
}
