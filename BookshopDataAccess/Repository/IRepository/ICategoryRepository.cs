using System.Linq.Expressions;
using BookshopDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BookshopModels.Models;

namespace BookshopDataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{ 
    void Update(Category Entity);

}