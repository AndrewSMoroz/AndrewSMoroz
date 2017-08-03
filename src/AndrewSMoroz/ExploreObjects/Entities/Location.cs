using System;
using System.Collections.Generic;

namespace ExploreConsole.Entities
{
    public partial class Location
    {
        public Location()
        {
            Items = new HashSet<Item>();
            LocationConnectionFromLocations = new HashSet<LocationConnection>();
            LocationConnectionToLocations = new HashSet<LocationConnection>();
        }

        public int Id { get; set; }
        public int MapId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInitialLocation { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<LocationConnection> LocationConnectionFromLocations { get; set; }
        public virtual ICollection<LocationConnection> LocationConnectionToLocations { get; set; }
        public virtual Map Map { get; set; }
    }
}
