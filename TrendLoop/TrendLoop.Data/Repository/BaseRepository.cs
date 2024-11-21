using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Repository.Interfaces;

namespace TrendLoop.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId> where TType : class
    {
        // define application dbContext
        private readonly TrendLoopDbContext dbContext;
       
        // define the Entity DbSet
        private readonly DbSet<TType> dbSet;

        public BaseRepository(TrendLoopDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TType>();
        }

        public TType GetById(TId id)
        {
            TType entity = this.dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet
                .FindAsync(id);

            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }
    }
}
