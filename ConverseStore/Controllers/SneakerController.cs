using Application.Interfaces;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConverseStore.Controllers
{
    public class SneakerController : Controller
    {
        private readonly ISneakerRepository _sneakerRepository;
        public SneakerController(ISneakerRepository sneakerRepository)
        {
            _sneakerRepository = sneakerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sneakers = await _sneakerRepository.GetAllAsync();
            return View(sneakers);
        }

        public IActionResult Delete(string Id)
        {
            _sneakerRepository.Remove(_sneakerRepository.GetById(Id));
            return RedirectToAction("Index", "Sneaker");
        }

        public IActionResult Create()
        {
            var reloadSafety = new Sneaker();
            return View(reloadSafety);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sneaker sneaker)
        {
            if (!ModelState.IsValid)
            {
                return View(sneaker);
            }
            await _sneakerRepository.AddAsync(sneaker);
            return RedirectToAction("Index", "Sneaker");
        }

        public async Task<IActionResult> Detail(string Id)
        {
            var sneaker = await _sneakerRepository.GetByIdAsync(Id);
            return View(sneaker);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var sneaker = await _sneakerRepository.GetByIdAsync(Id);
            var sneakerVM = new SneakerVM()
            {
                Id = Id,
                Size =sneaker.Size,
                Color = sneaker.Color,
                Cost = sneaker.Cost ,
                Count = sneaker.Count ,
                Name = sneaker.Name,
                OFF = sneaker.OFF
            };
            return View(sneakerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SneakerVM sneakerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sneakerVM);
            }
            var sneaker = new Sneaker()
            {
                Id = sneakerVM.Id,
                Size = sneakerVM.Size,
                Color = sneakerVM.Color,
                Cost = sneakerVM.Cost,
                Count = sneakerVM.Count,
                Name = sneakerVM.Name,
                OFF = sneakerVM.OFF
            };
            await _sneakerRepository.AddAsync(sneaker);
            return RedirectToAction("Index", "Sneaker");
        }
    }
}
