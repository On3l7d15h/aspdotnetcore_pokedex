using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SavePokemonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " - You need to fill the Name Input!")]
        public string Name { get; set; }

        [Required(ErrorMessage = " - You need to fill the ImageUrl Input!")]
        public string ImageUrl { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "The first type is required!")]
        public int? Type1 { get; set; }
        public int? Type2 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid region")]
        public int? Region { get; set; }
    }
}
