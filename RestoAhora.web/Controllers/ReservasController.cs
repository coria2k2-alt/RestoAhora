using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestoAhora.web.Controllers;

public class ReservasController:Controller
{
    private readonly IReservaMesaRepository _reservaRepository;
    private readonly IMesaRepository _mesaRepository; // Para cargar el dropdown de mesas

    public ReservasController(IReservaMesaRepository reservaRepository, IMesaRepository mesaRepository)
    {
        _reservaRepository = reservaRepository;
        _mesaRepository = mesaRepository;
    }

    public async Task<IActionResult> Index()
    {
        var reservas = await _reservaRepository.GetAllAsync();
        return View(reservas);
    }

    public async Task<IActionResult> Crear()
    {
        var mesas = await _mesaRepository.GetAllAsync();
        ViewBag.Mesas = new SelectList(mesas, "Id", "Numero");
        return View();
    }

    // POST: /Reservas/Crear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(ReservaMesa reserva)
    {
        // 1. Asignar valores por defecto para evitar fallas de validación
        reserva.Estado = EstadoReserva.pendiente;
        reserva.FechaCreacionUtc = DateTime.UtcNow;

        if (reserva.FechaHoraFinUtc == default)
        {
            reserva.FechaHoraFinUtc = reserva.FechaHoraInicioUtc.AddHours(2);
        }

        // 2. Limpiar validaciones de propiedades de navegación / opcionales
        ModelState.Remove("Mesa");
        ModelState.Remove("Estado");
        ModelState.Remove("TelefonoCliente");
        ModelState.Remove("EmailCliente");

        // 3. Validación de Solapamiento: Verificar si la mesa ya está ocupada en ese horario
        var reservasExistentes = await _reservaRepository.GetAllAsync();
        bool estaOcupada = reservasExistentes.Any(r =>
            r.MesaId == reserva.MesaId &&
            r.Estado != EstadoReserva.cancelada &&
            ((reserva.FechaHoraInicioUtc >= r.FechaHoraInicioUtc && reserva.FechaHoraInicioUtc < r.FechaHoraFinUtc) ||
             (reserva.FechaHoraFinUtc > r.FechaHoraInicioUtc && reserva.FechaHoraFinUtc <= r.FechaHoraFinUtc) ||
             (reserva.FechaHoraInicioUtc <= r.FechaHoraInicioUtc && reserva.FechaHoraFinUtc >= r.FechaHoraFinUtc)));

        if (estaOcupada)
        {
            ModelState.AddModelError("", "La mesa seleccionada ya tiene una reserva activa en ese rango horario.");
        }

        // 4. Si pasa todas las validaciones, guardamos
        if (ModelState.IsValid)
        {
            await _reservaRepository.AddAsync(reserva);
            await _reservaRepository.SaveChangesAsync();

            // Actualizamos el estado de la mesa elegida a Reservada
            var mesa = await _mesaRepository.GetByIdAsync(reserva.MesaId);
            if (mesa != null)
            {
                mesa.Estado = EstadoMesa.reservada;
                _mesaRepository.Update(mesa);
                await _mesaRepository.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Si falla por algún motivo, recargamos el combo de mesas
        var mesas = await _mesaRepository.GetAllAsync();
        ViewBag.Mesas = new SelectList(mesas, "Id", "Numero", reserva.MesaId);
        return View(reserva);
    }

    // GET: /Reservas/Editar/5
    public async Task<IActionResult> Editar(int id)
    {
        var reserva = await _reservaRepository.GetByAsync(id);
        if (reserva == null)
        {
            return NotFound();
        }

        // Cargar combo de mesas seleccionando por defecto la mesa actual
        var mesas = await _mesaRepository.GetAllAsync();
        ViewBag.Mesas = new SelectList(mesas, "id", "Numero", reserva.MesaId);
        return View(reserva);
    }

    // POST: /Reservas/Editar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, ReservaMesa reservaMesa)
    {
        if(id !=reservaMesa.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _reservaRepository.Update(reservaMesa);
            await _reservaRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var mesas = await _mesaRepository.GetAllAsync();
        ViewBag.Mesas = new SelectList(mesas, "Id", "Numero", reservaMesa.MesaId);
        return View(reservaMesa);
    }

    // GET: /Reservas/Cancelar/5
    public async Task<IActionResult> Cancelar(int id)
    {
        var reserva = await _reservaRepository.GetByAsync(id);
        if (reserva == null)
        {
            return NotFound();
        }

        return View(reserva);
    }

    // POST: /Reservas/Cancelar/5
    [HttpPost, ActionName("Cancelar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CancelarConfirmado(int id)
    {
        var reserva = await _reservaRepository.GetByAsync(id);
        if (reserva != null)
        {
            // 1. Cancelación lógica de la reserva
            reserva.Estado = EstadoReserva.cancelada;
            _reservaRepository.Update(reserva);

            // 2. Liberación de la mesa asociada
            var mesa = await _mesaRepository.GetByIdAsync(reserva.MesaId);
            if (mesa != null)
            {
                mesa.Estado = EstadoMesa.disponible;
                _mesaRepository.Update(mesa);
            }

            // 3. Impactamos ambos cambios juntos en la base de datos
            await _reservaRepository.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Método helper dentro de ReservasController
    private async Task<bool> ExisteSolapamiento(int mesaId, DateTime inicio, DateTime fin, int? reservaIdActual = null)
    {
        var reservas = await _reservaRepository.GetAllAsync();

        return reservas.Any(r =>
            r.MesaId == mesaId &&
            r.Id != reservaIdActual && // Excluye la misma reserva al editar
            r.Estado != EstadoReserva.cancelada &&
            ((inicio >= r.FechaHoraInicioUtc && inicio < r.FechaHoraFinUtc) ||
             (fin > r.FechaHoraInicioUtc && fin <= r.FechaHoraFinUtc) ||
             (inicio <= r.FechaHoraInicioUtc && fin >= r.FechaHoraFinUtc)));
    }
}
