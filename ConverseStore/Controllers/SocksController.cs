using Application.Interfaces;
using Context.Models;
using ConverseStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConverseStore.Controllers
{
    public class SocksController : Controller
    {
        private readonly ISocksRepository _socksRepository;
        private readonly IPhotoService _photoService;
        public SocksController(IPhotoService photoService, ISocksRepository socksRepository)
        {
            _socksRepository = socksRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            var allSocks = await _socksRepository.GetAllAsync();
            return View(allSocks);
        }

        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            var reloadSafety = new SocksVM();
            return View(reloadSafety);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SocksVM socksVM)
        {
            if (!ModelState.IsValid)
            {
                return View(socksVM);
            }
            var result = await _photoService.AddPhotoAsync(socksVM.Image);
            var socks = new Socks()
            {
                Name = socksVM.Name,
                Cost = socksVM.Cost,
                Count = socksVM.Count,
                Size = socksVM.Size,
                OFF = 0 , 
                Image = result.Url.ToString()
            };
            await _socksRepository.AddAsync(socks);
            return RedirectToAction("Index", "Socks");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            await _socksRepository.RemoveAsync(await _socksRepository.GetByIdAsync(Id));
            return RedirectToAction("Index","Socks");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
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
                Name = socks.Name,
                OFF = socks.OFF
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
            var result = await _photoService.AddPhotoAsync(socksVM.Image);

            var socks = new Socks()
            {
                Id = socksVM.Id,
                Cost = socksVM.Cost,
                Name = socksVM.Name,
                Count = socksVM.Count,
                Size = socksVM.Size,
                OFF = socksVM.OFF ,
                Image = result.Url.ToString()
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
