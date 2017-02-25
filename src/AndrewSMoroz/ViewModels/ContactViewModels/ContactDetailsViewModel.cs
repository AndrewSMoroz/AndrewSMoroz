using AndrewSMoroz.ViewModels.ContactPhoneViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.ContactViewModels
{

    public class ContactDetailsViewModel
    {

        public int ID { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        [Display(Name = "Type")]
        public string ContactType { get; set; }

        [Display(Name = "Type")]
        public int ContactTypeID { get; set; }

        public IEnumerable<ContactPhoneViewModel> PhoneNumbers { get; set; }

    }

}
