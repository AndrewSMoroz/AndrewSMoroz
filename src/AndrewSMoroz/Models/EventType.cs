using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class EventType : LookupItem
    {

        #region Navigation Properties

        public ICollection<Event> Events { get; set; }

        #endregion

    }

}
