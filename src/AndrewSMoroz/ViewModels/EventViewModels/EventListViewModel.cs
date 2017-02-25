using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.EventViewModels
{

    public class EventListViewModel
    {

        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm:ss tt}")]
        [Display(Name ="Date and Time")]
        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        [Display(Name = "Type")]
        public string EventType { get; set; }

    }

}
