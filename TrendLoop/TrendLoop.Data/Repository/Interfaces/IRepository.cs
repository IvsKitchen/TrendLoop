using System.Linq.Expressions;

namespace TrendLoop.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        // get type by Id
        TType GetById(TId id);

        // get type by Id async
        Task<TType> GetByIdAsync(TId id);

        // get all types, detached from DB
        IEnumerable<TType> GetAll();

        // get all types, detached from DB async
        Task<IEnumerable<TType>> GetAllAsync();

        // get all types, attached to DB
        IQueryable<TType> GetAllAttached();
    }
}
