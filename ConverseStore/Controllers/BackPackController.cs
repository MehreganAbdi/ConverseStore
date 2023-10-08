using Application.Interfaces;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConverseStore.Controllers
{
    public class BackPackController : Controller
    {
        private readonly IBackPackRepository _backPackRepository;
        private readonly IPhotoService _photoService;

        public BackPackController(IBackPackRepository backPackRepository, IPhotoService photoService)
        {
            _backPackRepository = backPackRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index(string searching)
        {
            var backPacks = await _backPackRepository.GetAllAsync();
            if(searching != null)
            {
                backPacks = backPacks.Where(b => b.Name.Contains(searching));
            }
            return View(backPacks);
        }

        public IActionResult Create()
        {
            
            var reloadSafety = new BackPackVM();
            return View(reloadSafety);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BackPackVM backPackVM)
        {
            if (!ModelState.IsValid)
            {
                return View(backPackVM);
            }
            var result = await _photoService.AddPhotoAsync(backPackVM.Image);

            var backPack = new BackPack()
            {
                Name = backPackVM.Name,

                Color = backPackVM.Color,
                Cost = backPackVM.Cost,
                OFF = 0,
                Count = backPackVM.Count,
                Image = result.Url.ToString()
            };
            await _backPackRepository.AddAsync(backPack);
            return RedirectToAction("Index", "BackPack");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var backPack = await _backPackRepository.GetByIdAsync(Id);
            if (backPack == null)
            {
                return RedirectToAction("Index", "BackPack");
            }
            var backPackVM = new BackPackVM()
            {
                Id = backPack.Id,
                Name = backPack.Name,

                Color = backPack.Color,
                Cost = backPack.Cost,
                OFF = backPack.OFF,
                Count = (int)backPack.Count
            };
            return View(backPackVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BackPackVM backPackVM)
        {
            if (!ModelState.IsValid)
            {
                return View(backPackVM);
            }
            var result = await _photoService.AddPhotoAsync(backPackVM.Image);
            var backPack = new BackPack()
            {
                Id = (int)backPackVM.Id,
                Name = backPackVM.Name,

                Color = backPackVM.Color,
                Cost = backPackVM.Cost,
                OFF = backPackVM.OFF,
                Count = backPackVM.Count,
                Image = result.Url.ToString()
            };
            _backPackRepository.Update(backPack);
            return RedirectToAction("Index", "BackPack");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            await _backPackRepository.RemoveAsync(await _backPackRepository.GetByIdAsync(Id));
            return RedirectToAction("Index", "BackPack");
        }
        public async Task<IActionResult> Detail(int Id)
        {
            return View(await _backPackRepository.GetByIdAsync(Id));
        }
    }
}
