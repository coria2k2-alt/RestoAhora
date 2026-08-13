using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface ICategoriaProductoRepository
    {
        Task<IEnumerable<CategoriaProducto>> GetAllAsync();
        Task<CategoriaProducto?> GetByIdAsync(int id);
        Task AddAsync(CategoriaProducto categoria);
        void Update(CategoriaProducto categoria);
        void Delete(CategoriaProducto categoria);
        Task SaveChangesAsync();
    }
}
