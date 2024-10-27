using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZadanieASP1.Data;
using ZadanieASP1.Models;
using ZadanieASP1.Repository;

namespace ZadanieASP1.Controllers
{
    [Authorize(Policy = "AdminOrManager")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        // GET: users
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        // GET: users/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email")] ApplicationUser user, string Password)
        {
            if (ModelState.IsValid)
            {
                // Tworzenie nowego użytkownika
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                // Obsłuż błędy podczas tworzenia użytkownika
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(user);
        }

        // GET: users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email")] ApplicationUser user, string? newPassword)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            // Debugowanie ModelState
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        foreach (var error in entry.Value.Errors)
                        {
                            Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _userRepository.GetByIdAsync(user.Id);
                    if (existingUser == null)
                    {
                        return NotFound(); // Użytkownik został usunięty przez inny proces
                    }

                    // Aktualizacja danych użytkownika
                    existingUser.UserName = user.UserName;
                    existingUser.Email = user.Email;

                    // Jeśli nowe hasło zostało podane, zmień hasło
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                        var result = await _userManager.ResetPasswordAsync(existingUser, token, newPassword);
                        if (!result.Succeeded)
                        {
                            // Obsłuż błędy zmiany hasła
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(user);
                        }
                    }

                    await _userRepository.UpdateAsync(existingUser);
                    await _userRepository.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingUser = await _userRepository.GetByIdAsync(user.Id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(user);
        }





        // GET: users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //blokowanie
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Nie znaleziono użytkownika." });
                }

                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // Ustaw blokadę na stałe

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Użytkownik został zablokowany" });
                }
                else
                {
                    return Json(new { success = false, message = "Nie udało się zaktualizować użytkownika.", errors = result.Errors });
                }
            }
            catch (Exception ex)
            {
                // Zwróć szczegóły błędu, by uzyskać więcej informacji
                return Json(new { success = false, message = "Wystąpił błąd podczas blokowania użytkownika.", error = ex.Message });
            }
        }

        //odblokowoanie

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnblockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LockoutEnd = null; // Usunięcie blokady
                await _userManager.ResetAccessFailedCountAsync(user); // Zresetowanie liczby nieudanych prób
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Użytkownik został odblokowany" });
                }
            }
            return Json(new { success = false, message = "Nie udało się odblokować użytkownika" });
        }


        // POST: users/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(id);
                await _userRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TogglePasswordRestrictions(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Przełącz stan DisablePasswordRestrictions
                user.DisablePasswordRestrictions = !user.DisablePasswordRestrictions;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var status = user.DisablePasswordRestrictions ? "disabled" : "enabled";
                    return Json(new { success = true, message = $"Password restrictions have been {status} for the user." });
                }
            }
            return Json(new { success = false, message = "Failed to toggle password restrictions." });
        }


    }
}
