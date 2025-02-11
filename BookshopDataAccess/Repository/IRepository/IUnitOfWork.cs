using System.Linq.Expressions;

namespace BookshopDataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }

    IProductRepository Product { get; }
    void Save();
}
