using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context) { }
}
