using System.Linq.Expressions;

namespace TrendLoop.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        // Get type by Id
        TType GetById(TId id);

        // Get type by Id async
        Task<TType> GetByIdAsync(TId id);

        // Get all types, detached from DB
        IEnumerable<TType> GetAll();

        // Get all types, detached from DB async
        Task<IEnumerable<TType>> GetAllAsync();

        // Get all types, attached to DB
        IQueryable<TType> GetAllAttached();

        // Get first or default type by given predicate
        TType FirstOrDefault(Func<TType, bool> predicate);

        // Get first or default type by given predicate async
        Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);

        // Get all types, attached to DB
        Task AddAsync(TType item);

        // Update type 
        bool Update(TType item);

        // Update type async
        Task<bool> UpdateAsync(TType item);
    }
}
