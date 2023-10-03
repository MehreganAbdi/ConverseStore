using Application.Interfaces;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConverseStore.Controllers
{
    public class SocksController : Controller
    {
        private readonly ISocksRepository _socksRepository;
        public SocksController(ISocksRepository socksRepository)
        {
            _socksRepository = socksRepository;
        }
        public async Task<IActionResult> Index()
        {
            var allSocks = await _socksRepository.GetAllAsync();
            return View(allSocks);
        }

        public IActionResult Create()
        {
            var reloadSafety = new Socks();
            return View(reloadSafety);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Socks socks)
        {
            if (!ModelState.IsValid)
            {
                return View(socks);
            }
            await _socksRepository.AddAsync(socks);
            return RedirectToAction("Index", "Socks");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _socksRepository.RemoveAsync(await _socksRepository.GetByIdAsync(Id));
            return RedirectToAction("Index","Socks");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var socks = await _socksRepository.GetByIdAsync(Id);
            if (socks == null)
            {
                return RedirectToAction("Index", "Socks");
            }
            var socksVM = new SocksVM()
            {
                Id = socks.Id,
                Size = socks.Size,
                Cost = socks.Cost,
                Count = socks.Count,
                Name = socks.Name
            };
            return View(socksVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SocksVM socksVM)
        {
            if (!ModelState.IsValid)
            {
                return View(socksVM);
            }
            var socks = new Socks()
            {
                Id = socksVM.Id,
                Cost = socksVM.Cost,
                Name = socksVM.Name,
                Count = socksVM.Count,
                Size = socksVM.Size
            };
            _socksRepository.Update(socks);
            return RedirectToAction("Index", "Socks");
        }


        public async Task<IActionResult> Detail(int Id)
        {
            var socks = await _socksRepository.GetByIdAsync(Id);
            return socks != null ? View(socks) : RedirectToAction("Index", "Socks"); 
        }
    }
}
