using Domain.Entities;

namespace Application.Interfaces;

public interface IReservaMesaRepository
{
    Task<ReservaMesa?> GetByAsync(int id);
    Task<IEnumerable<ReservaMesa>> GetAllAsync();
    Task AddAsync(ReservaMesa reserva);
    void Update(ReservaMesa reserva);
    Task SaveChangesAsync();
}