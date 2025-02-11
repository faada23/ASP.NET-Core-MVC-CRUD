using System.Linq.Expressions;
using BookshopDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BookshopModels.Models;

namespace BookshopDataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{ 
    void Update(Product Entity);

}