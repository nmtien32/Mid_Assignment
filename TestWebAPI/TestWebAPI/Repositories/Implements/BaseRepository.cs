using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Test.Data;
using TestWebApi.Repositories.Interfaces;

namespace TestWebApi.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly BookContext _context;

    public BaseRepository(BookContext context)
    {
        _dbSet = context.Set<T>();
        _context = context;
    }

    public T Create(T entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public IDatabaseTransaction DatabaseTransaction()
    {
        return new EntityDatabaseTransaction(_context);
    }

    public bool Delete(T entity)
    {
        _dbSet.Remove(entity);

        return true;
    }

    public T? Get(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet.FirstOrDefault() : _dbSet.FirstOrDefault(predicate);
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet : _dbSet.Where(predicate);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;
    }
}