using Microsoft.AspNetCore.Mvc;
//added
using Application.Services;
using Application.ViewModels;
using Database;

namespace Pokedex.Controllers
{
    public class PokemonController : Controller
    {
        private readonly PokemonService _pkmnService;

        public PokemonController(ApplicationContext db) 
        {
            _pkmnService = new(db);
        }
        public async Task<IActionResult> Index()
        {
            var pkmnList = await _pkmnService.GetPokemons();

            return View(pkmnList);
        }

        #region CreateMethod

        public async Task<IActionResult> Create()
        {
            ViewBag.Types = await _pkmnService.GetTypes();
            ViewBag.Regions = await _pkmnService.GetRegions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePokemonViewModel newPkmn)
        {
            ViewBag.Types = await _pkmnService.GetTypes();
            ViewBag.Regions = await _pkmnService.GetRegions();

            if (ModelState.IsValid)
            {
                await _pkmnService.PostNewPokemon(newPkmn);
                return RedirectToRoute(new
                {
                    Controller = "Pokemon",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region UpdateMethod

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Types = await _pkmnService.GetTypes();
            ViewBag.Regions = await _pkmnService.GetRegions();
            var _specificPkmn = await _pkmnService.GetSpecificPokemon(id);
            return View(_specificPkmn);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SavePokemonViewModel pkmn)
        {
            if (ModelState.IsValid)
            {
                await _pkmnService.UpdatePokemon(pkmn);
                return RedirectToRoute(new 
                {
                    Controller = "Pokemon",
                    Action = "Index"
                });
            }

            return View();
        }
        #endregion

        #region DeleteMethods

        public async Task<IActionResult> Delete(int id)
        {
            var _specificPkmn = await _pkmnService.GetSpecificPokemon(id);
            return View(_specificPkmn);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (ModelState.IsValid)
            {
                await _pkmnService.DeletePokemon(id);
                return RedirectToRoute(new 
                { 
                    Controller = "Pokemon",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion 
    }
}
