﻿using BlogTalks.Infrastructure.Data.DataContext;
using BlogTalks.Infrastructure.Reposotories;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context, DbSet<TEntity> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public TEntity? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }
}
