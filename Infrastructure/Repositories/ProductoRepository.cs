using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            //include solo puede tomar objetos no o listas de navegacion no sobre enteros
            return await _context.Producto.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetByCategoriasAsync(int categoriaID)
        {
           return await _context.Producto
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaProductoID == categoriaID)
                .ToListAsync();
        }

        public async Task AddAsync(Producto produc)
        {
           await _context.Producto.AddAsync(produc);
        }

        public void Update(Producto produc)
        {
            _context.Producto.Update(produc);
        }

        public void Delete(Producto produc)
        {
            _context.Producto.Remove(produc);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
