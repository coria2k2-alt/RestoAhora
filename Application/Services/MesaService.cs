using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepositoty)
    {
        _mesaRepository = mesaRepositoty;
    }

    public async Task<IEnumerable<MesaDto>> ObtenerTodasAsync()
    {
        var mesas = await _mesaRepository.GetAllAsync();

        return mesas.Select(m => new MesaDto {
            Id = m.Id,
            Numero = m.Numero,
            Capacidad = m.Capacidad,
            Ubicacion = m.Ubicacion,
            Estado = m.Estado,
            Activo = m.Activo
        });
    }

    public async Task<MesaDto?> ObtenerPorIdAsync(int id)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);

        if (mesa == null) return null;

        return new MesaDto
        {
            Id = mesa.Id,
            Numero = mesa.Numero,
            Capacidad = mesa.Capacidad,
            Ubicacion = mesa.Ubicacion,
            Estado = mesa.Estado,
            Activo = mesa.Activo
        };
    }

    public async Task CrearMesaAsync(MesaDto dto)
    {
        var nuevaMesa = new Mesa
        {
            Numero = dto.Numero,
            Capacidad = dto.Capacidad,
            Ubicacion = dto.Ubicacion,
            Estado = dto.Estado,
            Activo = true
        };

        await _mesaRepository.AddAsync(nuevaMesa);
        await _mesaRepository.SaveChangesAsync();
    }
}