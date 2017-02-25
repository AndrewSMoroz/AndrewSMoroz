using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.LookupViewModels
{

    public class LookupItemViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter an integer value for Sequence.")]
        public int Sequence { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
        public string Description { get; set; }
    }

}
