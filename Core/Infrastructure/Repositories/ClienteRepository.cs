using Microsoft.EntityFrameworkCore;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Core.Interfaces;
using PC1Web.CORE.Infrastructure.Data;
namespace PC1Web.CORE.Infrastructure.Repositories;

public class ClienteRepository(TallerMecanicoContext db) : IClienteRepository
{
    public Task<List<Cliente>> GetAll() => db.Cliente.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
    public Task<Cliente?> GetById(int id) => db.Cliente.FirstOrDefaultAsync(e => e.Id == id);
    public async Task Create(Cliente entity)
    {
        db.Cliente.Add(entity);
        await db.SaveChangesAsync();
    }
    public async Task Save() { await db.SaveChangesAsync(); }
    public async Task Delete(Cliente entity)
    {
        db.Cliente.Remove(entity);
        await db.SaveChangesAsync();
    }
    public Task<bool> TieneVehiculos(int id) => db.Vehiculo.AnyAsync(v => v.ClienteId == id);
}
