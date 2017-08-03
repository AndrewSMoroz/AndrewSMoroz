using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreConsole.DTO
{

    /// <summary>
    /// Wrapper object representing a user's session with a given map.
    /// It includes a map definition, and the user's current state within that map.
    /// </summary>
    public class MapSession
    {
        public MapDefinition MapDefinition { get; set; }
        public MapState MapState { get; set; }
    }

}
