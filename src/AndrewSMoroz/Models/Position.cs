using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class Position
    {

        public int ID { get; set; }
        public int CompanyID { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DatePosted { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }

        #region Navigation Properties

        public Company Company { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<PositionContact> PositionContacts { get; set; }

        #endregion

    }

}
