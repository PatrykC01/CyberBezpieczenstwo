using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZadanieASP1.Data;
using ZadanieASP1.Models;
using ZadanieASP1.Repository;

namespace ZadanieASP1.Controllers
{
    [Authorize(Policy = "AdminOrManager")]
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IUserRepository _customerRepository;
        private readonly IMapper _mapper;
        public ReservationsController(IReservationRepository reservationRepository, ITripRepository tripRepository, IUserRepository customerRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _tripRepository = tripRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationRepository.GetAllAsync();

            var viewModel = _mapper.Map<List<ReservationViewModel>>(reservations);

            return View(viewModel);
        }





        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.GetByIdAsync(Convert.ToInt32(id));

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetAllAsync(), "ID", "ID");
            ViewData["TripID"] = new SelectList(await _tripRepository.GetAllAsync(), "TripID", "TripID");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,TripID,CustomerID,ReservationDate,DateOfDeparture,DateOfReturn")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _reservationRepository.InsertAsync(reservation);
                await _reservationRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetAllAsync(), "ID", "ID", reservation.CustomerID);
            ViewData["TripID"] = new SelectList(await _tripRepository.GetAllAsync(), "TripID", "TripID", reservation.TripID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.GetByIdAsync(Convert.ToInt32(id));

            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetAllAsync(), "ID", "ID", reservation.CustomerID);
            ViewData["TripID"] = new SelectList(await _tripRepository.GetAllAsync(), "TripID", "TripID", reservation.TripID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,TripID,CustomerID,ReservationDate,DateOfDeparture,DateOfReturn")] Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationRepository.UpdateAsync(reservation);
                    await _reservationRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var res = await _reservationRepository.GetByIdAsync(reservation.ReservationID);

                    if (res == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(await _customerRepository.GetAllAsync(), "ID", "ID", reservation.CustomerID);
            ViewData["TripID"] = new SelectList(await _tripRepository.GetAllAsync(), "TripID", "TripID", reservation.TripID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.GetByIdAsync(Convert.ToInt32(id));

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            
            if (reservation != null)
            {
                await _reservationRepository.DeleteAsync(id);
                await _reservationRepository.SaveAsync();
               return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

       
    }
}
