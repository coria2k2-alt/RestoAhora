using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class MesaRepository : IMesaRepository
{
    // Campo privado para guardar la instancia de la base de datos
    private readonly ApplicationDbContext _context;

    public MesaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Mesa?> GetByIdAsync(int id)
    {
        return await _context.Mesas.FindAsync(id);
    }

    public async Task<IEnumerable<Mesa>> GetAllAsync()
    {
        return await _context.Mesas.ToListAsync();
    }

    public async Task AddAsync(Mesa mesa)
    {
        await _context.Mesas.AddAsync(mesa);
    }

    public void Update(Mesa mesa)
    {
        _context.Mesas.Update(mesa);
    }

    // Guarda los cambios en SQL Server
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}