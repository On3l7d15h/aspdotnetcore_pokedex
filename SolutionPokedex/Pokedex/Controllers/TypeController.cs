using Microsoft.AspNetCore.Mvc;
using Application.ViewModels;
//added
using Application.Services;
using Database;

namespace Pokedex.Controllers
{
    public class TypeController : Controller
    {
        private readonly TypeService _typeService;

        public TypeController(ApplicationContext db)
        {
            _typeService = new(db);
        }

        public async Task<IActionResult> Index()
        {
            var typeList = await _typeService.GetAllTypes();
            return View(typeList);
        }

        #region CreateTypeMethod

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePokemonTypeViewModel newType)
        {
            if (ModelState.IsValid)
            {
                await _typeService.PostNewType(newType);
                return RedirectToRoute(new
                {
                    Controller = "Type",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region UpdateTypeMethod

        public async Task<IActionResult> Update(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var specificType = await _typeService.GetSpecificTypeById(id);

            if (specificType == null)
            {
                return NotFound();
            }

            return View(specificType);

        }

        [HttpPost]
        public async Task<IActionResult> Update(SavePokemonTypeViewModel pkmnType)
        {
            if (ModelState.IsValid)
            {
                await _typeService.UpdateType(pkmnType);
                return RedirectToRoute(new
                {
                    Controller = "Type",
                    Action = "Index"
                });
            }

            return View();
            
        }

        #endregion

        #region DeleteTypeMethod

        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var specificType = await _typeService.GetSpecificTypeById(id);

            if(specificType == null)
            {
                return NotFound();
            }

            return View(specificType);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _typeService.DeleteType(id);
                return RedirectToRoute(new 
                { 
                    Controller = "Type",
                    Action = "Index"
                });
            }
            
            return View();
        }

        #endregion
    }
}
