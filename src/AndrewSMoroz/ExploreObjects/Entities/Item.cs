using System;
using System.Collections.Generic;

namespace ExploreConsole.Entities
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public bool Plural { get; set; }
        public string Determiner { get; set; }

        public virtual Location Location { get; set; }
    }
}
