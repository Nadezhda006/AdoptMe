using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptMe.Data;
using AdoptMe.Data.Entities;
using AdoptMe.Services.Abstractions;
using AdoptMe.DTOs;

namespace AdoptMe.Controllers
{
    public class VisitsController : Controller
    {
        private readonly IPetService _petService;
        private readonly IVetService _vetService;

        public VisitsController(IPetService petService, IVetService vetService)
        {
            _petService = petService;
            _vetService = vetService;
        }

        public async Task<IActionResult> Create()
        {
            var petVisitDto = new CreateVisitDTO()
            {
                VisitDate = DateTime.Now,
                Pets = (await _petService.GetAllAsync())
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList(),
                Vets = (await _vetService.GetAllAsync())
                .Select(vet => new SelectListItem { Value = vet.Id.ToString(), Text = $"{vet.FirstName} {vet.LastName}" })
                .ToList()
            };

            return View(petVisitDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitDTO visitDto)
        {
            if (ModelState.IsValid)
            {
                await _petService.AddCatVisitAsync(visitDto);

                return RedirectToAction("Index", "Pets");
            }
            return View(visitDto);
        }

    }
}
