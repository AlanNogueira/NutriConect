using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    public void Remove(T entity) => _context.Set<T>().Remove(entity);
}
