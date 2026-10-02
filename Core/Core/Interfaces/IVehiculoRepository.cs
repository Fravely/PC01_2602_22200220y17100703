using PC1Web.CORE.Core.Entities;
namespace PC1Web.CORE.Core.Interfaces;

public interface IVehiculoRepository
{
    Task<List<Vehiculo>> GetAll();
    Task<Vehiculo?> GetById(int id);
    Task Create(Vehiculo entity);
    Task Save();
    Task Delete(Vehiculo entity);
    Task<bool> ClienteExiste(int id);
    Task<bool> TieneOrdenes(int id);
}
