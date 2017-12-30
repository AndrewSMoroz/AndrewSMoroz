using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.PositionViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.CompanyViewModels
{

    public class CompanyDetailsViewModel
    {

        public int ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "State cannot be longer than 2 characters.")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "Postal Code cannot be longer than 10 characters.")]
        [DataType(DataType.PostalCode, ErrorMessage = "Please enter a valid Postal Code.")]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^$|^(\d{5})(?:-?(\d{4}))?$", ErrorMessage = "Postal code must be in the format 00000 or 00000-0000.")]
        public string PostalCode { get; set; }

        [Display(Name = "Is Recruiter")]
        public bool IsRecruiter { get; set; }

        public IEnumerable<PositionListViewModel> Positions { get; set; }
        public IEnumerable<PositionListViewModel> RecruiterPositions { get; set; }
        public IEnumerable<ContactListViewModel> Contacts { get; set; }

    }

}
