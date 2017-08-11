using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    /// <summary>
    /// Use this class to transform domain model entities into DTOs to send to clients
    /// </summary>
    public class ExploreDTOAdapter : IExploreDTOAdapter
    {

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a collection of Map entities to its DTO equivalent
        /// </summary>
        public List<ExploreObjects.DTO.Map> ConvertMaps(List<ExploreObjects.Entities.Map> maps)
        {

            List<ExploreObjects.DTO.Map> mapsDTO = new List<ExploreObjects.DTO.Map>();

            if (maps == null)
            {
                return mapsDTO;
            }

            foreach (ExploreObjects.Entities.Map map in maps)
            {
                ExploreObjects.DTO.Map m = new ExploreObjects.DTO.Map()
                {
                    Id = map.Id,
                    Name = map.Name
                };
                mapsDTO.Add(m);
            }

            return mapsDTO;

        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Builds a MapSession object that contains DTO versions of the entities.
        /// The DTO versions don't include the navigation properties.
        /// This is necessary because the location objects contain circular references to one another that cause
        /// problems when serializing to JSON for saving.
        /// </summary>
        public ExploreObjects.DTO.MapSession CreateMapSessionObject(ExploreObjects.Entities.Map map, List<ExploreObjects.Entities.Item> items)
        {

            // Validate that a map and at least one location is defined

            if (map == null || map.Locations == null || !map.Locations.Any())
            {
                throw new Exception("Could not find valid map and location data for MapID " + map.Id.ToString());
            }

            // Validate that exactly one initial location is defined

            List<ExploreObjects.Entities.Location> initialLocations = map.Locations
                                                                         .Where(l => l.IsInitialLocation == true)
                                                                         .ToList();

            if (initialLocations == null || !initialLocations.Any())
            {
                throw new Exception("No initial location defined for MapID " + map.Id.ToString());
            }

            if (initialLocations.Count > 1)
            {
                throw new Exception("More than one initial location defined for MapID " + map.Id.ToString());
            }

            // Transform the Entities objects into DTO objects

            ExploreObjects.DTO.MapSession newMapSession = new ExploreObjects.DTO.MapSession();

            newMapSession.MapDefinition = new ExploreObjects.DTO.MapDefinition();
            newMapSession.MapDefinition.Map = new ExploreObjects.DTO.Map() { Id = map.Id, Name = map.Name };
            List<ExploreObjects.DTO.Location> newLocations = new List<ExploreObjects.DTO.Location>();
            foreach (ExploreObjects.Entities.Location l in map.Locations)
            {
                ExploreObjects.DTO.Location newLocation = new ExploreObjects.DTO.Location() { Id = l.Id, Description = l.Description, IsInitialLocation = l.IsInitialLocation, MapId = l.MapId, Name = l.Name };
                List<ExploreObjects.DTO.LocationConnection> newLocationConnections = new List<ExploreObjects.DTO.LocationConnection>();
                foreach (ExploreObjects.Entities.LocationConnection lc in l.LocationConnectionFromLocations)
                {
                    ExploreObjects.DTO.LocationConnection newLocationConnection = new ExploreObjects.DTO.LocationConnection () { Id = lc.Id, ToLocationId = lc.ToLocationId, Direction = lc.Direction };
                    newLocationConnections.Add(newLocationConnection);
                }
                newLocation.LocationConnections = newLocationConnections;
                newLocations.Add(newLocation);
            }
            newMapSession.MapDefinition.Map.Locations = newLocations;

            newMapSession.MapState = new ExploreObjects.DTO.MapState();
            newMapSession.MapState.MapID = map.Id;
            newMapSession.MapState.CurrentLocationID = initialLocations[0].Id;
            newMapSession.MapState.ActionResultMessages = new List<string>();
            List<ExploreObjects.DTO.Item> newItems = new List<ExploreObjects.DTO.Item>();
            foreach (ExploreObjects.Entities.Item i in items)
            {
                newItems.Add(new ExploreObjects.DTO.Item() { Id = i.Id, Description = i.Description, Determiner = i.Determiner, LocationId = i.LocationId, Name = i.Name, Plural = i.Plural });
            }
            newMapSession.MapState.Items = newItems;

            return newMapSession;

        }

    }

}
