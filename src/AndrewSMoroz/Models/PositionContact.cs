using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class PositionContact
    {

        public int PositionID { get; set; }
        public int ContactID { get; set; }
        public bool IsPrimaryContact { get; set; }

        public Position Position { get; set; }
        public Contact Contact { get; set; }

    }

}
