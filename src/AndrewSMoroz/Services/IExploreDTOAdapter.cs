using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public interface IExploreDTOAdapter
    {

        List<ExploreObjects.DTO.Map> ConvertMaps(List<ExploreObjects.Entities.Map> maps);
        ExploreObjects.DTO.MapSession CreateMapSessionObject(ExploreObjects.Entities.Map map, List<ExploreObjects.Entities.Item> items);

    }

}
