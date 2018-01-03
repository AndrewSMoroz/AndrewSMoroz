using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.EventViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.PositionViewModels
{

    public class PositionDetailsViewModel
    {

        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Posted")]
        public DateTime? DatePosted { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Recruiter")]
        public int? RecruiterCompanyID { get; set; }

        [Display(Name = "Recruiter Name")]
        public string RecruiterCompanyName { get; set; }

        [Display(Name = "Contacts")]
        public IEnumerable<int> ContactIDs { get; set; }
        public IEnumerable<ContactListViewModel> Contacts { get; set; }

        [Display(Name = "Recruiter Contacts")]
        public IEnumerable<int> RecruiterContactIDs { get; set; }
        public IEnumerable<ContactListViewModel> RecruiterContacts { get; set; }

        public IEnumerable<EventListViewModel> Events { get; set; }

    }

}
