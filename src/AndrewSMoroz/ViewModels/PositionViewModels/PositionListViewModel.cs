using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.PositionViewModels
{

    public class PositionListViewModel
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Posted")]
        public DateTime? DatePosted { get; set; }

        public int CompanyID { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        public int? RecruiterCompanyID { get; set; }

        [Display(Name = "Recruiter")]
        public string RecruiterCompanyName { get; set; }

        [Display(Name = "Most Recent Event")]
        public string MostRecentEventType { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm:ss tt}")]
        [Display(Name = "Date and Time")]
        public DateTime? MostRecentEventDateTime { get; set; }

    }

}
