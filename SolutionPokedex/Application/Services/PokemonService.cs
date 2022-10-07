using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Application.Repositories;
using Application.ViewModels;
using Database;
using Database.Models;

namespace Application.Services
{
    public class PokemonService
    {
        private readonly PokemonRepository _repository;

        public PokemonService(ApplicationContext db)
        {
            _repository = new(db);
        }

        #region GetPokemonsMethods
        public async Task<List<PokemonViewModel>> GetPokemons()
        {
            var regions = await _repository.Regions();
            var types = await _repository.Types();
            var pokemonsList = await _repository.GetAsyncPokemon();
            return pokemonsList
                .Select(data => new PokemonViewModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    ImageUrl = data.ImageUrl,
                    Region = regions.Single(reg => reg.Id == data?.RegionId).Name, 
                    Type1 = types.Single(typ => typ.Id == data?.PrimaryTypeId).Name,
                    Type2 = (data.SecondaryTypeId == null || data.SecondaryTypeId == 0) ? "" : types.Single(t => t.Id == data?.SecondaryTypeId).Name,
                })
                .ToList();
        }

        public async Task<List<PokemonViewModel>> GetFilteredPokemons(FilterPokemonViewModel filter)
        {
            var regions = await _repository.Regions();
            var types = await _repository.Types();
            var pokemonsList = await _repository.GetAsyncPokemon();
            var ToFilter = pokemonsList
                .Select(data => new PokemonViewModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    ImageUrl = data.ImageUrl,
                    Region = regions.Single(reg => reg.Id == data?.RegionId).Name,
                    Type1 = types.Single(typ => typ.Id == data?.PrimaryTypeId).Name,
                    Type2 = (data.SecondaryTypeId == null || data.SecondaryTypeId == 0) ? "" : types.Single(t => t.Id == data?.SecondaryTypeId).Name,
                    RegionCategoryId = data.RegionId,
                })
                .ToList();
        
            if(filter.Name != null && filter.Name != "")
            {
                return ToFilter.Where(pkmn => pkmn.Name.Contains(filter?.Name)).ToList();
            } 
            
            if(filter.CategoryId != null && filter.CategoryId != 0)
            {
                return ToFilter.Where(pkmn => pkmn.RegionCategoryId == filter?.CategoryId).ToList();
            }

            return ToFilter;
        }

        public async Task<SavePokemonViewModel> GetSpecificPokemon(int id)
        {
            var item = await _repository.GetSpecificPkmnById(id);
            return new SavePokemonViewModel
            {
                Name = item.Name,
                Id = item.Id,
                ImageUrl = item.ImageUrl,
                Region = item.RegionId,
                Type1 = item.PrimaryTypeId,
                Type2 = item?.SecondaryTypeId != null && item?.SecondaryTypeId != 0 ? item?.SecondaryTypeId : 0,
            };
        }

        public async Task<List<PokemonTypeViewModel>> GetTypes()
        {
            var types = await _repository.Types();
            return types
                    .Select(data => new PokemonTypeViewModel
                    {
                        Id = data.Id,
                        Name = data.Name,
                    })
                    .ToList();
        }

        public async Task<List<RegionViewModel>> GetRegions() 
        {
            var regions = await _repository.Regions();
            return regions
                    .Select(data => new RegionViewModel
                    {
                        Id = data.Id,
                        Name = data.Name,
                    })
                    .ToList();
        }
        #endregion

        #region PostPokemonsMethods

        public async Task PostNewPokemon(SavePokemonViewModel newPkmn)
        {
            await _repository.PostPokemonAsync(newPkmn);
        }

        #endregion

        #region UpdatePokemonsMethods

        public async Task UpdatePokemon(SavePokemonViewModel pkmn)
        {
            await _repository.UpdatePokemonAsync(new Pokemon 
            {
                Id = pkmn.Id,
                Name = pkmn.Name,
                ImageUrl = pkmn.ImageUrl,
                RegionId = (int)pkmn.Region,
                PrimaryTypeId = (int)pkmn.Type1,
                SecondaryTypeId = pkmn.Type2 == null || pkmn.Type2 == 0 ? null : pkmn?.Type2,
            });

        }

        #endregion

        #region DeletePokemonMethods
        
        public async Task DeletePokemon(int id)
        {
            var specificPokemon = await _repository.GetSpecificPkmnById(id);

            if (specificPokemon != null)
            {
                await _repository.DeletePokemonAsync(specificPokemon);
            }

        }

        #endregion 
    }
}
