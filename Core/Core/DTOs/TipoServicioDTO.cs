using System.ComponentModel.DataAnnotations;
namespace PC1Web.CORE.Core.DTOs;

public class TipoServicioCreateDTO
{
    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    [Range(typeof(decimal), "0", "99999999.99")]
    public decimal PrecioBase { get; set; }
}
public class TipoServicioUpdateDTO : TipoServicioCreateDTO
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
}
public class TipoServicioListDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioBase { get; set; }
}
