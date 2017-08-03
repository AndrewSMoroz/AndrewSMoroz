using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreConsole.Entities
{

    public partial class MapSessionSave
    {

        public int Id { get; set; }
        public int MapId { get; set; }
        public string SaveData { get; set; }
        public DateTime SaveDateTime { get; set; }

        public virtual Map Map { get; set; }

    }

}
