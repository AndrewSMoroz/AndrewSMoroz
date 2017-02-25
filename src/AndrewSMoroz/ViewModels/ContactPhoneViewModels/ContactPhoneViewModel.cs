using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.ContactPhoneViewModels
{

    public class ContactPhoneViewModel
    {

        public int ID { get; set; }
        public int ContactID { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactFullName { get; set; }

        [Required]
        [Display(Name = "Phone Type")]
        public int ContactPhoneTypeID { get; set; }

        [Display(Name = "Contact Phone Type")]
        public string ContactPhoneType { get; set; }

        [Display(Name = "Primary")]
        public bool IsPrimaryPhone { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(12, ErrorMessage = "Phone Number cannot be longer than 12 characters.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid Phone Number.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format 000-000-0000")]
        public string PhoneNumber { get; set; }

        [StringLength(6, ErrorMessage = "Extension cannot be longer than 6 characters.")]
        public string Extension { get; set; }

    }

}
