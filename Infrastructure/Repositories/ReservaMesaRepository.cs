using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence; // Importante para reconocer ApplicationDbContext
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservaMesaRepository:IReservaMesaRepository
{
    private readonly ApplicationDbContext _context;

    public ReservaMesaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReservaMesa?> GetByAsync(int id)
    {
        return await _context.Reservas
            .Include(r => r.Mesa)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(ReservaMesa reserva)
    {
        await _context.Reservas.AddAsync(reserva);
    }

    public async Task<IEnumerable<ReservaMesa>> GetAllAsync()
    {
        return await _context.Reservas.Include(r => r.Mesa).ToListAsync();
    }

    public void Update(ReservaMesa reserva)
    {
        _context.Reservas.Update(reserva);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

  
}
