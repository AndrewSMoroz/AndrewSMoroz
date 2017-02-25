using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static AndrewSMoroz.Enums;

namespace AndrewSMoroz.ViewModels.EventViewModels
{

    public class EventDetailsViewModel
    {

        public int ID { get; set; }
        public int PositionID { get; set; }

        [Display(Name = "Position")]
        public string PositionTitle { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int EventTypeID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Time { get; set; }

        public AMPMDesignator AMPM { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

    }

}
