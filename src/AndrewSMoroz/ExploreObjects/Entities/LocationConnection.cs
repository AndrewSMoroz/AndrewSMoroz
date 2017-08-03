using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExploreConsole.Entities
{
    public partial class LocationConnection
    {
        public int Id { get; set; }
        public string Direction { get; set; }
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }

        public virtual Location FromLocation { get; set; }
        public virtual Location ToLocation { get; set; }
    }
}
