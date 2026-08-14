using NutriConect.Business.Interfaces;
using NutriConect.Data.Context;

namespace NutriConect.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context) => _context = context;

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
