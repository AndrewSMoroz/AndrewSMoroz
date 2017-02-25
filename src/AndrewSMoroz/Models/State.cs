using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Models
{

    public class State
    {

        public int ID { get; set; }
        public int Sequence { get; set; }

        [MaxLength(2)]
        public string Abbreviation { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

    }

}
