using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreConsole.DTO
{

    /// <summary>
    /// Represents a user's current state within a map.
    /// </summary>
    public class MapState
    {

        public int MapID { get; set; }
        public int CurrentLocationID { get; set; }
        public List<Item> Items { get; set; } 
        public string RequestedAction { get; set; }
        public string RequestedActionTarget { get; set; }
        public List<string> ActionResultMessages { get; set; }

    }

}
