using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.ExploreViewModels
{

    public class SetupViewModel
    {

        [Required]
        [Display(Name = "Map")]
        public int MapID { get; set; }

        public List<ExploreObjects.DTO.Map> Maps { get; set; }

    }

}

