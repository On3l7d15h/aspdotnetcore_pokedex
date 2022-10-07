using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type1 { get; set; }
        public string? Type2 { get; set; }
        public string Region { get; set; }

        public int? RegionCategoryId { get; set; }
    }
}
