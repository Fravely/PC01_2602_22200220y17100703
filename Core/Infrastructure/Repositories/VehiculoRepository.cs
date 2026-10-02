using Microsoft.EntityFrameworkCore;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Core.Interfaces;
using PC1Web.CORE.Infrastructure.Data;
namespace PC1Web.CORE.Infrastructure.Repositories;

public class VehiculoRepository(TallerMecanicoContext db) : IVehiculoRepository
{
    public Task<List<Vehiculo>> GetAll() => db.Vehiculo.AsNoTracking().Include(e => e.Cliente).OrderBy(e => e.Id).ToListAsync();
    public Task<Vehiculo?> GetById(int id) => db.Vehiculo.Include(e => e.Cliente).FirstOrDefaultAsync(e => e.Id == id);
    public async Task Create(Vehiculo entity)
    {
        db.Vehiculo.Add(entity);
        await db.SaveChangesAsync();
    }
    public async Task Save() { await db.SaveChangesAsync(); }
    public async Task Delete(Vehiculo entity)
    {
        db.Vehiculo.Remove(entity);
        await db.SaveChangesAsync();
    }
    public Task<bool> ClienteExiste(int id) => db.Cliente.AnyAsync(c => c.Id == id);
    public Task<bool> TieneOrdenes(int id) => db.OrdenServicio.AnyAsync(o => o.VehiculoId == id);
}
