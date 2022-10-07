using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Database;
using Application.Repositories;
using Application.ViewModels;

namespace Application.Services
{
    public class RegionService
    {
        private readonly RegionRepository _regRepository;

        public RegionService(ApplicationContext db)
        {
            _regRepository = new(db);
        }

        #region GetMethods

        public async Task<List<RegionViewModel>> GetRegions()
        {
            var listRegion = await _regRepository.GetAsyncRegions();
            return listRegion
                .Select(r => new RegionViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .ToList();
        }

        public async Task<SaveRegionViewModel> GetSpecificRegion(int id)
        {
            var specificRegion = await _regRepository.GetSpecificRegionById(id);
            return new SaveRegionViewModel { Id = specificRegion.Id, Name = specificRegion.Name };   
        }

        #endregion

        #region PostMethods

        public async Task PostRegion(SaveRegionViewModel newRegion)
        {
            await _regRepository.CreateRegionAsync(newRegion);
        }

        #endregion

        #region UpdateMethods

        public async Task UpdateRegion(SaveRegionViewModel updateRegion)
        {
            await _regRepository.UpdateRegionAsync(updateRegion);
        }

        #endregion

        #region UpdateMethods

        public async Task DeleteRegion(int id)
        {
            var region = await _regRepository.GetSpecificRegionById(id);

            if(region != null)
            {
                await _regRepository.DeleteRegionAsync(region);
            }

        }

        #endregion
    }
}
