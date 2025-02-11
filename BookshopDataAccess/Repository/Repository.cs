using System.Linq.Expressions;
using BookshopDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BookshopDataAccess.Repository.IRepository;
using BookshopModels.Models;

namespace BookshopDataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{   
    public readonly ApplicationDbContext _db;
    public DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }

    public void Add(T Entity)
    {
        dbSet.Add(Entity);
    }

    public void Delete(T Entity)
    {
        dbSet.Remove(Entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter,string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        if(!string.IsNullOrEmpty(includeProperties)){
            foreach(var includeProp in includeProperties
            .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)){
                query = query.Include(includeProp);
            }
        }
        return query.FirstOrDefault();
    }
}
