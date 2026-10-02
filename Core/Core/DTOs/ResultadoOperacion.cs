namespace PC1Web.CORE.Core.DTOs;

public enum EstadoOperacion { Correcto, NoEncontrado, Invalido, Conflicto }
public record ResultadoOperacion<T>(EstadoOperacion Estado, T? Datos = default, string? Error = null);
