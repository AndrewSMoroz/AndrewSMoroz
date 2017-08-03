using System;
using System.Collections.Generic;

namespace ExploreConsole.Entities
{
    public partial class Map
    {
        public Map()
        {
            Locations = new HashSet<Location>();
            MapSessionSaves = new HashSet<MapSessionSave>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<MapSessionSave> MapSessionSaves { get; set; }

    }
}
