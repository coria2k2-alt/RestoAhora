using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task <IEnumerable<Producto>> GetByCategoriasAsync(int categoriaID);
        Task AddAsync(Producto produc);
        void Update(Producto produc);
        void Delete(Producto produc);
        Task SaveChangesAsync();
    }
}
