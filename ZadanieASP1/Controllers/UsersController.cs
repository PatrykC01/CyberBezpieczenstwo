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

        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
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
        public async Task<IActionResult> Create([Bind("UserName,Email")] IdentityUser user, string Password)
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email")] IdentityUser user, string? newPassword)
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
    }
}
