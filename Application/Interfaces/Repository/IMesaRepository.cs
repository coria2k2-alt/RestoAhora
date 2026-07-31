using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IMesaRepository
{
    Task<Mesa?> GetByIdAsync(int id);

    Task<IEnumerable<Mesa>> GetAllAsync();

    Task AddAsync(Mesa mesa);

    void Update(Mesa mesa);

    Task SaveChangesAsync();
}