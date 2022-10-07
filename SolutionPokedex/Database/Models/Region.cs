using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Region
    {
        // Regions Properties
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property 
        public ICollection<Pokemon>? Pokemons { get; set; }
    }
}
