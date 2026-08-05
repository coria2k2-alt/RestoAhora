using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RestoAhora.web.Controllers
{
    public class MesasController : Controller
    {
        private readonly IMesaRepository _mesaRepository;

        public MesasController(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var mesas = await _mesaRepository.GetAllAsync();

            // Filtra para que solo pasen a la vista las mesas que están en Activo = true
            var mesasActivas = mesas.Where(m => m.Activo);

            return View(mesasActivas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                await _mesaRepository.AddAsync(mesa);

                await _mesaRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(mesa);
        }


        //Sirve para mostrar el formulario cargado
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var mesa = await _mesaRepository.GetByIdAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            return View(mesa);
        }

        //Sirve para procesar los datos al enviar el formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                 _mesaRepository.Update(mesa);
                await _mesaRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(mesa);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mesa = await _mesaRepository.GetByIdAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            return View(mesa);
        }

        [HttpPost,ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var mesa = await _mesaRepository.GetByIdAsync(id);

            if (mesa != null)
            {
                mesa.Activo = false;
                _mesaRepository.Update(mesa);
                await _mesaRepository.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        
    }
}