using Microsoft.EntityFrameworkCore;
using PC1Web.CORE.Core.Entities;
using PC1Web.CORE.Core.Interfaces;
using PC1Web.CORE.Infrastructure.Data;
namespace PC1Web.CORE.Infrastructure.Repositories;

public class OrdenServicioRepository(TallerMecanicoContext db) : IOrdenServicioRepository
{
    public Task<List<OrdenServicio>> GetAll() => db.OrdenServicio.AsNoTracking().Include(e => e.Vehiculo).Include(e => e.TipoServicio).OrderBy(e => e.Id).ToListAsync();
    public Task<OrdenServicio?> GetById(int id) => db.OrdenServicio.Include(e => e.Vehiculo).Include(e => e.TipoServicio).FirstOrDefaultAsync(e => e.Id == id);
    public async Task Create(OrdenServicio entity)
    {
        db.OrdenServicio.Add(entity);
        await db.SaveChangesAsync();
    }
    public async Task Save() { await db.SaveChangesAsync(); }
    public async Task Delete(OrdenServicio entity)
    {
        db.OrdenServicio.Remove(entity);
        await db.SaveChangesAsync();
    }
    public Task<bool> VehiculoExiste(int id) => db.Vehiculo.AnyAsync(v => v.Id == id);
    public Task<bool> TipoServicioExiste(int id) => db.TipoServicio.AnyAsync(t => t.Id == id);
}
