using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class LookupItem
    {

        public int ID { get; set; }
        public int Sequence { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }


    }

}
