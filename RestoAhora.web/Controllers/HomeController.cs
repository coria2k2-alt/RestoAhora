using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Enums;
using RestoAhora.web.Models;
using System.Diagnostics;

namespace RestoAhora.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMesaRepository _mesaRepository;
        private readonly IReservaMesaRepository _reservaRepository;

        public HomeController(IMesaRepository mesaRepository, IReservaMesaRepository reservaRepository)
        {
            _mesaRepository = mesaRepository;
            _reservaRepository = reservaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var mesas = (await _mesaRepository.GetAllAsync()) ?? Enumerable.Empty<Domain.Entities.Mesa>();
            var reservas = (await _reservaRepository.GetAllAsync()) ?? Enumerable.Empty<Domain.Entities.ReservaMesa>();

            var hoy = DateTime.UtcNow.Date;

            ViewBag.TotalMesas = mesas.Count();
            ViewBag.MesasDisponibles = mesas.Count(m => m.Estado == EstadoMesa.disponible);
            ViewBag.MesasReservadas = mesas.Count(m => m.Estado == EstadoMesa.reservada);
            ViewBag.ReservasHoy = reservas.Count(r => r.FechaHoraInicioUtc.Date == hoy && r.Estado != EstadoReserva.cancelada);

            var proximasReservas = reservas
                .Where(r => r.FechaHoraInicioUtc.Date >= hoy && r.Estado != EstadoReserva.cancelada)
                .OrderBy(r => r.FechaHoraInicioUtc)
                .Take(5)
                .ToList();

            return View(proximasReservas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}