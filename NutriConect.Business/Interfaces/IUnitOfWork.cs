namespace NutriConect.Business.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
