using ExploreObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public interface IExploreBusinessManager
    {

        Task<MapSession> GetInitialMapSessionAsync(int mapID);
        Task<List<Map>> GetMapListAsync();
        MapState ProcessAction(MapSession mapSession);

    }

}
