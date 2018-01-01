using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.ViewModels.ContactViewModels
{

    public class ContactListViewModel
    {

        public int ID { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        public bool CompanyIsRecruiter { get; set; }

        public int CompanyID { get; set; }

        [Display(Name = "Type")]
        public string ContactType { get; set; }

        [Display(Name = "Primary Phone")]
        public string PrimaryPhone { get; set; }

        [Display(Name = "Primary Phone Type")]
        public string PrimaryPhoneType { get; set; }

        public string NameAndType
        {
            get
            {
                return (FullName ?? "unknown name") + " - " + (ContactType ?? "unknown type");
            }
        }

    }

}
