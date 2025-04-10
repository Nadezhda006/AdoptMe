using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptMe.Data;
using AdoptMe.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using AdoptMe.Services.Abstractions;
using AdoptMe.DTOs;
namespace AdoptMe.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPetService _petService;

        public PetsController(IPetService petService, ApplicationDbContext context)
        {
            _petService = petService;
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            return View(await _petService.GetAllAsync());
        }

        public IActionResult ShowSearchForm()
        {
            return _petService != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
        }

        public async Task<IActionResult> ShowSearchResults(string SearchName,string SearchType,string SearchBreed)
        {
            return View("Index",_petService.GetByNameBreedAndType(SearchName,SearchBreed,SearchType));
        }

        

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petService.GetByIdAsync(id.Value);
                
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        
        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Breed,Sex,Age,Color,Weight,ImageURL,Price,Location,Details")] PetDTO petDTO)
        {
            if (ModelState.IsValid)
            {
                await _petService.CreateAsync(petDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(petDTO);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petService.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Breed,Sex,Age,Color,Weight,ImageURL,Price,Location,Details")] PetDTO petDTO)
        {
            if (id != petDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _petService.UpdateAsync(petDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PetExistsAsync(petDTO.Id))
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
            return View(petDTO);
        }

        // GET: Pets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petService.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _petService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PetExistsAsync(int id)
        {
            var item = await _petService.GetByIdAsync(id);
            return item != null;
        }
    }
}
