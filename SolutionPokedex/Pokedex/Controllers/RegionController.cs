using Microsoft.AspNetCore.Mvc;
//added
using Application.Services;
using Application.ViewModels;
using Database;

namespace Pokedex.Controllers
{
    public class RegionController : Controller
    {
        private readonly RegionService _regService;

        public RegionController(ApplicationContext db)
        {
            _regService = new(db);
        }
        public async Task<IActionResult> Index()
        {
            var regions = await _regService.GetRegions();
            return View(regions);
        }

        #region CreateActionMethods

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveRegionViewModel region)
        {
            if (ModelState.IsValid)
            {
                await _regService.PostRegion(region);
                return RedirectToRoute(new
                {
                    Controller = "Region",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region UpdateActionMethods

        public async Task<IActionResult> Update(int id)
        {
            var specificRegion = await _regService.GetSpecificRegion(id);
            return View(specificRegion);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveRegionViewModel updateRegion)
        {
            if (ModelState.IsValid)
            {
                await _regService.UpdateRegion(updateRegion);
                return RedirectToRoute(new
                {
                    Controller = "Region",
                    Action = "Index"
                });
            }

            return View();
        }

        #endregion

        #region DeleteActionMethods

        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var specificRegion = await _regService.GetSpecificRegion(id);

            if(specificRegion == null)
            {
                return NotFound();
            }

            return View(specificRegion);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            await _regService.DeleteRegion(id);
            return RedirectToRoute(new 
            {
                Controller = "Region",
                Action = "Index"
            });
        }

        #endregion
    }
}
