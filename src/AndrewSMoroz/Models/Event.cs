using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class Event
    {

        public int ID { get; set; }
        public int PositionID { get; set; }
        public int EventTypeID { get; set; }
        public DateTime DateTime { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }

        #region Navigation Properties

        public Position Position { get; set; }
        public EventType EventType { get; set; }
        public ICollection<EventContact> EventContacts { get; set; }

        #endregion

    }

}
