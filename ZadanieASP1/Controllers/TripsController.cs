using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZadanieASP1.Data;
using ZadanieASP1.Models;
using ZadanieASP1.Repository;
using ZadanieASP1.Services;

namespace ZadanieASP1.Controllers
{
    
    public class TripsController : Controller
    {
        private readonly ITripService _tripService;
        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: Trips
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var paginatedTrips = await _tripService.GetTripsPerPageAsync(pageNumber ?? 1, 3, searchString);

            return View(paginatedTrips);
        }





        // GET: Trips/Details/5
        [Authorize(Roles = "Admin, Manager, Member")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _tripService.GetByIdAsync(Convert.ToInt32(id));

            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        [Authorize(Policy = "AdminOrManager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "AdminOrManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripID,Title,Description,Price,TripDate,Duration")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                await _tripService.InsertAsync(trip);
                //Call SaveAsync to Insert the data into the database
                await _tripService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Edit/5
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _tripService.GetByIdAsync(Convert.ToInt32(id));

            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "AdminOrManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("TripID,Title,Description,Price,TripDate,Duration")] Trip trip)
        {
            if (id != trip.TripID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tripService.UpdateAsync(trip);
                    await _tripService.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var tri = await _tripService.GetByIdAsync(trip.TripID);

                    if (tri == null)
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
            return View(trip);
        }

        // GET: Trips/Delete/5
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _tripService.GetByIdAsync(Convert.ToInt32(id));

            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [Authorize(Policy = "AdminOrManager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _tripService.GetByIdAsync(id);

            if (trip != null)
            {
                await _tripService.DeleteAsync(id);
                await _tripService.SaveAsync();
                 return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        
    }
}
