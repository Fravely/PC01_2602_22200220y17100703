using PC1Web.CORE.Core.DTOs;
namespace PC1Web.CORE.Core.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteListDTO>> GetAll();
    Task<ClienteListDTO?> GetById(int id);
    Task<ResultadoOperacion<ClienteListDTO>> Create(ClienteCreateDTO dto);
    Task<ResultadoOperacion<bool>> Update(int id, ClienteUpdateDTO dto);
    Task<ResultadoOperacion<bool>> Delete(int id);
}
