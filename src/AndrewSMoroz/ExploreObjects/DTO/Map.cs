using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreConsole.DTO
{

    public class Map
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Location> Locations { get; set; }

    }

}
