using Application.Interfaces;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConverseStore.Controllers
{
    public class SneakerController : Controller
    {
        private readonly ISneakerRepository _sneakerRepository;
        private readonly IPhotoService _photoService;
        public SneakerController(IPhotoService photoService, ISneakerRepository sneakerRepository)
        {
            _sneakerRepository = sneakerRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var sneakers = await _sneakerRepository.GetAllAsync();
            return View(sneakers);
        }

        public IActionResult Delete(int Id)
        {
            if(!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            _sneakerRepository.Remove(_sneakerRepository.GetById(Id));
            return RedirectToAction("Index", "Sneaker");
        }

        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            var reloadSafety = new SneakerVM();
            return View(reloadSafety);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SneakerVM sneakerVM)
        {

            var result = await _photoService.AddPhotoAsync(sneakerVM.Image);
            var sneaker = new Sneaker()
            {
                Name = sneakerVM.Name,
                Color = sneakerVM.Color,
                Cost = sneakerVM.Cost,
                Count = sneakerVM.Count,
                Size = sneakerVM.Size,
                OFF = 0,
                Image = result.Url.ToString()
            };
            await _sneakerRepository.AddAsync(sneaker);
            return RedirectToAction("Index", "Sneaker");
        }

        public async Task<IActionResult> Detail(int Id)
        {
           
            var sneaker = await _sneakerRepository.GetByIdAsync(Id);
            return View(sneaker);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
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
            var result = await _photoService.AddPhotoAsync(sneakerVM.Image);

            var sneaker = new Sneaker()
            {
                Id = sneakerVM.Id,
                Size = sneakerVM.Size,
                Color = sneakerVM.Color,
                Cost = sneakerVM.Cost,
                Count = sneakerVM.Count,
                Name = sneakerVM.Name,
                OFF = sneakerVM.OFF,
                Image = result.Url.ToString()
            };
             _sneakerRepository.Update(sneaker);
            return RedirectToAction("Index", "Sneaker");
        }
    }
}
