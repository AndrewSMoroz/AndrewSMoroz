using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class EventContact
    {

        public int EventID { get; set; }
        public int ContactID { get; set; }

        #region Navigation Properties

        public Event Event { get; set; }
        public Contact Contact { get; set; }

        #endregion

    }

}
