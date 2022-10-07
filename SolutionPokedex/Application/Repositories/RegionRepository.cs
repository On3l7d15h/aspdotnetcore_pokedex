using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
//added
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class RegionRepository
    {
        private readonly ApplicationContext _regRepository;

        public RegionRepository(ApplicationContext db) 
        {
            _regRepository = db;
        }

        #region GetMethods

        public async Task<List<Region>> GetAsyncRegions()
        {
            return await _regRepository.Set<Region>().ToListAsync();
        }

        public async Task<Region> GetSpecificRegionById(int id)
        {
            return await _regRepository.Set<Region>().FindAsync(id);
        }

        #endregion

        #region PostMethods

        public async Task CreateRegionAsync(SaveRegionViewModel _region)
        {
            await _regRepository.Set<Region>().AddAsync(new Region { Id = 0, Name = _region.Name });
            await _regRepository.SaveChangesAsync();
        }

        #endregion

        #region UpdateMethods

        public async Task UpdateRegionAsync(SaveRegionViewModel region)
        {
            _regRepository.Entry(new Region { Id = region.Id, Name = region.Name }).State = EntityState.Modified;
            await _regRepository.SaveChangesAsync();
        }

        #endregion

        #region DeleteMethods

        public async Task DeleteRegionAsync(Region region)
        {
            _regRepository.Set<Region>().Remove(region);
            await _regRepository.SaveChangesAsync();
        }

        #endregion
    }
}
