using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreConsole.DTO
{

    public class LocationConnection
    {

        public int Id { get; set; }
        public string Direction { get; set; }
        public int ToLocationId { get; set; }

    }

}
