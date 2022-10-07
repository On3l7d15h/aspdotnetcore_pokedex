using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Database;
using Application.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class TypeRepository
    {
        private readonly ApplicationContext _typeRepository;

        public TypeRepository (ApplicationContext db)
        {
            _typeRepository = db;
        }

        #region GetMethods

        public async Task<List<Database.Models.Type>> GetTypesAsync()
        {
            return await _typeRepository.Set<Database.Models.Type>().ToListAsync();
        }

        public async Task<Database.Models.Type> GetTypeByIdAsync(int id)
        {
            return await _typeRepository.Set<Database.Models.Type>().FindAsync(id);
        }

        #endregion

        #region PostMethods

        public async Task PostNewType(SavePokemonTypeViewModel newType)
        {
            await _typeRepository.Set<Database.Models.Type>().AddAsync(new Database.Models.Type
            {
                Name = newType.Name
            });

            await _typeRepository.SaveChangesAsync();
        }

        #endregion

        #region UpdateMethods

        public async Task UpdateType(Database.Models.Type type)
        {
            _typeRepository.Entry(type).State = EntityState.Modified;
            await _typeRepository.SaveChangesAsync();
        }

        #endregion

        #region DeleteMethods

        public async Task? DeleteType(Database.Models.Type delType)
        {
            _typeRepository.Set<Database.Models.Type>().Remove(delType);
            await _typeRepository.SaveChangesAsync();
        }

        #endregion
    }
}
