using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using System.Diagnostics;
//added
using Application.Services;
using Application.ViewModels;
using Database;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonService _pkmnService;

        public HomeController(ApplicationContext db)
        {
            _pkmnService = new(db);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Regions = await _pkmnService.GetRegions();
            var pkmnList = await _pkmnService.GetPokemons();
            return View(pkmnList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? CategoryId = 0, string? Name = "")
        {
            FilterPokemonViewModel filter = new() { CategoryId = CategoryId, Name = Name };
            ViewBag.Regions = await _pkmnService.GetRegions();
            var pkmnList = await _pkmnService.GetFilteredPokemons(filter);
            return View(pkmnList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}