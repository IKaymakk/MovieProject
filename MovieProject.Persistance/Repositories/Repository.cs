using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MovieContext _movieContext;
    private readonly DbSet<T> _dbSet;

    public Repository(MovieContext movieContext)
    {
        _movieContext = movieContext;
        _dbSet = _movieContext.Set<T>();
    }


    public async Task<int> Count()
    {
        return await _movieContext.Set<T>().CountAsync();
    }

    public async Task CreateAsync(T entity)
    {
        _movieContext.Set<T>().Add(entity);
        await _movieContext.SaveChangesAsync();
    }

    public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _movieContext.Set<T>().Where(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _movieContext.Set<T>().AsNoTracking().ToListAsync();
    }



    public async Task<T> GetByIdAsync(int id)
    {
        return await _movieContext.Set<T>().FindAsync(id);
    }

    public async Task RemoveAsync(T entity)
    {
        _movieContext.Set<T>().Remove(entity);
        await _movieContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _movieContext.Set<T>().Update(entity);
        await _movieContext.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _movieContext.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
    {
        return await _movieContext.Set<T>()
                                .AsNoTracking() 
                                .AnyAsync(filter);
    }
}