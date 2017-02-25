using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class ContactPhone
    {

        public int ID { get; set; }
        public int ContactID { get; set; }
        public int ContactPhoneTypeID { get; set; }
        public bool IsPrimaryPhone { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(6)]
        public string Extension { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }

        #region Navigation Properties

        public Contact Contact { get; set; }
        public ContactPhoneType ContactPhoneType { get; set; }

        #endregion

    }

}
