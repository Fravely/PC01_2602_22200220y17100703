using PC1Web.CORE.Core.Entities;
namespace PC1Web.CORE.Core.Interfaces;

public interface IOrdenServicioRepository
{
    Task<List<OrdenServicio>> GetAll();
    Task<OrdenServicio?> GetById(int id);
    Task Create(OrdenServicio entity);
    Task Save();
    Task Delete(OrdenServicio entity);
    Task<bool> VehiculoExiste(int id);
    Task<bool> TipoServicioExiste(int id);
}
