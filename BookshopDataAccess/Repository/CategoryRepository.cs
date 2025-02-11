using BookshopDataAccess.Data;
using BookshopModels.Models; 
using BookshopDataAccess.Repository.IRepository;

namespace BookshopDataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }

}
