using PC1Web.CORE.Core.Entities;
namespace PC1Web.CORE.Core.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAll();
    Task<Cliente?> GetById(int id);
    Task Create(Cliente entity);
    Task Save();
    Task Delete(Cliente entity);
    Task<bool> TieneVehiculos(int id);
}
