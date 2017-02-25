using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class Contact
    {

        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int ContactTypeID { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return ((FirstName ?? "") + " " + (LastName ?? "")).Trim();
            }
        }
        public string FullNameLastNameFirst
        {
            get
            {
                return ((LastName ?? "") + (string.IsNullOrEmpty(FirstName) ? "" : ", " + (FirstName ?? ""))).Trim();
            }
        }

        [MaxLength(256)]
        public string UserName { get; set; }

        #region Navigation Properties

        public Company Company { get; set; }
        public ContactType ContactType { get; set; }
        public ICollection<ContactPhone> ContactPhones { get; set; }
        public ICollection<PositionContact> PositionContacts { get; set; }
        public ICollection<EventContact> EventContacts { get; set; }

        #endregion

    }

}
