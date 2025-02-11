using System.Linq.Expressions;

namespace BookshopDataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? includeProperties = null);

    T Get(Expression<Func<T, bool>> filter,string? includeProperties = null);

    void Add(T Entity);

    void Delete(T Entity);

    void DeleteRange(IEnumerable<T> entities);
}