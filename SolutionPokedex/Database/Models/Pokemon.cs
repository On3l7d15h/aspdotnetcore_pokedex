using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Pokemon
    {
        //Pokemons attributes
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int PrimaryTypeId { get; set; }
        public int? SecondaryTypeId { get; set; }
        public int RegionId { get; set; }

        // Navigation Property
        public Database.Models.Type? Type { get; set; }
        public Region? Region { get; set; }
    }
}
