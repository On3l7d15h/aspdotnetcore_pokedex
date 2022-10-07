using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Application.Repositories;
using Application.ViewModels;
using Database;

namespace Application.Services
{
    public class TypeService
    {
        private readonly TypeRepository _typeRepository;

        public TypeService(ApplicationContext db)
        {
            _typeRepository = new(db);
        }

        #region GetMethods

        public async Task<List<PokemonTypeViewModel>> GetAllTypes()
        {
            var typeList = await _typeRepository.GetTypesAsync();
            return typeList
                .Select(t => new PokemonTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToList();
        }

        public async Task<SavePokemonTypeViewModel> GetSpecificTypeById(int id)
        {
            var specificType = await _typeRepository.GetTypeByIdAsync(id);
            return new SavePokemonTypeViewModel { Id = specificType.Id, Name = specificType.Name };
        }

        #endregion


        #region PostMethods

        public async Task PostNewType(SavePokemonTypeViewModel newType)
        {
            await _typeRepository.PostNewType(newType);
        }

        #endregion

        #region UpdateMethods

        public async Task UpdateType(SavePokemonTypeViewModel existingType)
        {
            await _typeRepository.UpdateType(new Database.Models.Type
            {
                Id = existingType.Id,
                Name = existingType.Name
            });
        }

        #endregion

        #region DeleteMethods
        
        public async Task DeleteType(int id)
        {
            if (id != 0)
            {
                var specificItem = await _typeRepository.GetTypeByIdAsync(id);

                if (specificItem != null)
                {
                    await _typeRepository.DeleteType(specificItem);
                }
            }
        }

        #endregion
    }
}
