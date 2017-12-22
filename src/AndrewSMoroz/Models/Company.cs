using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class Company
    {

        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }

        public bool IsRecruiter { get; set; }

        #region Navigation Properties

        public ICollection<Position> Positions { get; set; }
        public ICollection<Position> RecruiterPositions { get; set; }
        public ICollection<Contact> Contacts { get; set; }

        #endregion

    }

}
