using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class ContactType : LookupItem
    {

        #region Navigation Properties

        public ICollection<Contact> Contacts { get; set; }

        #endregion

    }

}
