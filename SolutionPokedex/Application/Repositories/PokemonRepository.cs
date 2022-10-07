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
    public class PokemonRepository
    {
        private readonly ApplicationContext _db;
        public PokemonRepository(ApplicationContext db)
        {
            _db = db;
        }

        #region GetMethods
        public async Task<List<Pokemon>> GetAsyncPokemon()
        {
            return await _db.Set<Pokemon>().ToListAsync();
        }

        public async Task<Pokemon> GetSpecificPkmnById(int id)
        {
            var specificPkmn = await _db.Set<Pokemon>().FindAsync(id);

            if(specificPkmn != null)
            {
                return specificPkmn;
            }

            return null;
        }

        public async Task<List<Database.Models.Type>> Types()
        {
            return await _db.Set<Database.Models.Type>().ToListAsync();
        }

        public async Task<List<Region>> Regions()
        {
            return await _db.Set<Region>().ToListAsync();
        }
        #endregion

        #region PostMethods

        public async Task PostPokemonAsync(SavePokemonViewModel newPkmn)
        {
            await _db.Set<Pokemon>().AddAsync(new Pokemon
                {
                    Id = 0,  
                    Name = newPkmn.Name,
                    ImageUrl = newPkmn.ImageUrl,
                    PrimaryTypeId = (int)(newPkmn?.Type1),
                    SecondaryTypeId = newPkmn?.Type2 != 0 && newPkmn?.Type2 != null ? newPkmn?.Type2 : null,
                    RegionId = (int)(newPkmn?.Region)
                });
            await _db.SaveChangesAsync();
        }

        #endregion

        #region UpdateMethos

        public async Task UpdatePokemonAsync(Pokemon specificPokemon)
        {
            _db.Entry(specificPokemon).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        #endregion

        #region DeleteMethods

        public async Task DeletePokemonAsync(Pokemon pkmn)
        {
            _db.Set<Pokemon>().Remove(pkmn);
            await _db.SaveChangesAsync();
        }

        #endregion 
    }
}
