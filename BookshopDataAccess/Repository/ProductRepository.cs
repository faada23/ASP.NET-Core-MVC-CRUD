using BookshopDataAccess.Data;
using BookshopModels.Models; 
using BookshopDataAccess.Repository.IRepository;

namespace BookshopDataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{   
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        
    }

    public void Update(Product obj)
    {
        var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Title = obj.Title;
            objFromDb.Author = obj.Author;
            objFromDb.ISBN = obj.ISBN;
            objFromDb.ListPrice = obj.ListPrice;
            objFromDb.Price = obj.Price;
            objFromDb.Price50 = obj.Price50;
            objFromDb.Price100 = obj.Price100;
            objFromDb.Description = obj.Description;
            objFromDb.CategoryId = obj.CategoryId;

            if(obj.ImageUrl != null){
                objFromDb.ImageUrl = obj.ImageUrl;
            }
        }
    }

}