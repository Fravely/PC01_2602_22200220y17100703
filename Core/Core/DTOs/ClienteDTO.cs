using System.ComponentModel.DataAnnotations;
namespace PC1Web.CORE.Core.DTOs;

public class ClienteCreateDTO
{
    [Required, StringLength(50)]
    public string Paterno { get; set; } = string.Empty;
    [Required, StringLength(50)]
    public string Materno { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Nombres { get; set; } = string.Empty;
    [Required, StringLength(150), EmailAddress]
    public string Correo { get; set; } = string.Empty;
    [Required, StringLength(20)]
    public string Telefono { get; set; } = string.Empty;
}
public class ClienteUpdateDTO : ClienteCreateDTO
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
}
public class ClienteListDTO
{
    public int Id { get; set; }
    public string Paterno { get; set; } = string.Empty;
    public string Materno { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}
