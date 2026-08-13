using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoriaProductoRepository : ICategoriaProductoRepository
    {

        private readonly ApplicationDbContext _context;

        //constructor
        public CategoriaProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaProducto>> GetAllAsync()
        {
            return await _context.CategoriaProducto
                .Include(c => c.Productos)
                .ToListAsync();
        }
        public async Task<CategoriaProducto?> GetByIdAsync(int id)
        {
            return await _context.CategoriaProducto
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task AddAsync(CategoriaProducto categoria)
        {
            await _context.CategoriaProducto.AddAsync(categoria);
        }

        public void Update(CategoriaProducto categoria)
        {
            _context.CategoriaProducto.Update(categoria);
        }
        public void Delete(CategoriaProducto categoria)
        {
            _context.CategoriaProducto.Remove(categoria);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
