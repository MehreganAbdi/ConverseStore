using Application.Interfaces;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConverseStore.Controllers
{
    public class BackPackController : Controller
    {
        private readonly IBackPackRepository _backPackRepository;
        public BackPackController(IBackPackRepository backPackRepository)
        {
            _backPackRepository = backPackRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var reloadSafety = new BackPack();
            return View(reloadSafety);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BackPack backPack)
        {
            if (!ModelState.IsValid)
            {
                return View(backPack);
            }
            await _backPackRepository.AddAsync(backPack);
            return RedirectToAction("Index", "BackPack");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var backPack = await _backPackRepository.GetByIdAsync(Id);
            if (backPack==null)
            {
                return RedirectToAction("Index", "BackPack");
            }
            var backPackVM = new BackPackVM()
            {
                Name = backPack.Name,
                Description = backPack.Description,
                Color = backPack.Color,
                Cost = backPack.Cost
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
            var backPack = new BackPack()
            {
                Id = (int)backPackVM.Id,
                Name = backPackVM.Name,
                Description = (string)backPackVM.Description,
                Color = backPackVM.Color,
                Cost = backPackVM.Cost
            };
            _backPackRepository.Update(backPack);
            return RedirectToAction("Index", "BackPack");
        }
    }
}
