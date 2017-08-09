using ExploreObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public interface IExploreDTOAdapter
    {

        MapSession CreateMapSessionObject(ExploreObjects.Entities.Map map, List<ExploreObjects.Entities.Item> items);

    }

}
