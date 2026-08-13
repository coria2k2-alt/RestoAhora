using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestoAhora.web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoRepository _productoRepo;
        private readonly ICategoriaProductoRepository _categoriaRepo;

        public ProductosController(IProductoRepository productoRepo, ICategoriaProductoRepository categoriaRepo)
        {
            _productoRepo = productoRepo;
            _categoriaRepo = categoriaRepo;
        }

        // GET: /Productos
        public async Task<IActionResult> Index()
        {
            var productos = await _productoRepo.GetAllAsync();
            return View(productos);
        }

        // GET: /Productos/Crear
        public async Task<IActionResult> Crear()
        {
            var categorias = await _categoriaRepo.GetAllAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            return View();
        }

        // POST: /Productos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                await _productoRepo.AddAsync(producto);
                await _productoRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categorias = await _categoriaRepo.GetAllAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaProductoID);
            return View(producto);
        }

        // GET: /Productos/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _productoRepo.GetByIdAsync(id);
            if (producto == null) return NotFound();

            var categorias = await _categoriaRepo.GetAllAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaProductoID);
            return View(producto);
        }

        // POST: /Productos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoRepo.Update(producto);
                await _productoRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categorias = await _categoriaRepo.GetAllAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaProductoID);
            return View(producto);
        }
    }
}