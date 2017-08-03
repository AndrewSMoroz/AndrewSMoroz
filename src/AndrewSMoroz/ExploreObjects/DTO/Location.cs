using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreConsole.DTO
{

    public class Location
    {

        public int Id { get; set; }
        public int MapId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInitialLocation { get; set; }
        public List<LocationConnection> LocationConnections { get; set; }

    }

}
