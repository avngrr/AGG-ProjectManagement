using System.Security.Claims;
using Application.Common.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T, TId> : IRepository<T, TId> where T : AuditableEntity<TId>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _table;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(object id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _table.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _table.AddAsync(entity);
        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        T current = _table.Find(entity.Id);
        _context.Entry(current).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(T entity)
    {
        _table.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}