using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public static class ExploreItemExtensions
    {

        //--------------------------------------------------------------------------------------------------------------
        public static string BuildGeneralDescription(this ExploreObjects.DTO.Item item)
        {
            return "There " + (item.Plural ? "are" : "is") + " " + item.Determiner + " " + item.Name + " here.";
        }

        //--------------------------------------------------------------------------------------------------------------
        public static string BuildInventoryDescription(this ExploreObjects.DTO.Item item)
        {
            return item.Determiner + " " + item.Name;
        }

    }

}
