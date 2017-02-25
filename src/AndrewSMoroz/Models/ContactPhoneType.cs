using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class ContactPhoneType : LookupItem
    {

        #region Navigation Properties

        public ICollection<ContactPhone> ContactPhones { get; set; }

        #endregion

    }

}
