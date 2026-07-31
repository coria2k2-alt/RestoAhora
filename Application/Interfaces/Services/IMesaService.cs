using Application.DTOs;

namespace Application.Interfaces;

public interface IMesaService
{
    // Obtiene todas las mesas procesadas y adaptadas como DTOs para la pantalla
    Task<IEnumerable<MesaDto>> ObtenerTodasAsync();

    // Obtiene una sola mesa adaptada como DTO por su ID (o null si no existe)
    Task<MesaDto?> ObtenerPorIdAsync(int id);

    // Recibe los datos enviados desde un formulario Web (DTO), valida las reglas y crea la mesa
    Task CrearMesaAsync (MesaDto dto);
}