using ExploreConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Data
{

    public static class ExploreDbInitializer
    {

        public static void Initialize(ExploreDbContext context)
        {

            // Use the presence of Maps to determine whether the database has already been populated
            if (context.Maps.Any())
            {
                return;
            }


            // NOTE: The array below may run a bit faster than the List<T> approach farther down, but using a List allows you to
            //       step through each .Add call, which can make debugging a little easier in the event that one of the objects
            //       throws an Exception as it is being created.  The whole array is initialized in a single statement.


            // Populate Map table
            Map[] maps = new Map[]
            {
                new Map { Name = "Main Street" },
                new Map { Name = "First Avenue" }
            };

            foreach (Map m in maps)
            {
                context.Maps.Add(m);
            }
            context.SaveChanges();


            // Populate Location table
            List<Location> locations = new List<Location>();

            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Entry Way", Description = null, IsInitialLocation = true });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Dining Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Kitchen", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Living Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Bonus Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Breakfast Nook", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Hallway Junction", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Champaign Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Laundry Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Garage", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Guest Bedroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Guest Bathroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Master Bedroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Master Bathroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Master Closet", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "Main Street").Id, Name = "Office", Description = null });

            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Entry Way", Description = null, IsInitialLocation = true });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Living Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Dining Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Kitchen", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Garage", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Pantry", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Downstairs Bathroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Stairway Landing", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Loft", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Laundry Room", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Stairway Overlook", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Guest Bathroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Guest Bedroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Office", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Master Bedroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Master Bathroom", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Master Toilet", Description = null });
            locations.Add(new Location { MapId = maps.Single(m => m.Name == "First Avenue").Id, Name = "Master Closet", Description = null });

            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();


            // Populate Map table
            List<LocationConnection> connections = new List<LocationConnection>();

            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Entry Way" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "Main Street").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Entry Way" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Entry Way" && l.Map.Name == "Main Street").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Entry Way" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, Direction = "NW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Bonus Room" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, Direction = "NE" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Bonus Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Direction = "SW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Champaign Room" && l.Map.Name == "Main Street").Id, Direction = "NW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Guest Bedroom" && l.Map.Name == "Main Street").Id, Direction = "NE" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Guest Bathroom" && l.Map.Name == "Main Street").Id, Direction = "SE" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Champaign Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, Direction = "SE" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Garage" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Garage" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Guest Bedroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, Direction = "SW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Guest Bathroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Hallway Junction" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "Main Street").Id, Direction = "NE" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, Direction = "NW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Closet" && l.Map.Name == "Main Street").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Closet" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "Main Street").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Closet" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "Main Street").Id, Direction = "SW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Closet" && l.Map.Name == "Main Street").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "Main Street").Id, ToLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, Direction = "N" });

            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Entry Way" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Entry Way" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Stairway Landing" && l.Map.Name == "First Avenue").Id, Direction = "U" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "First Avenue").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Downstairs Bathroom" && l.Map.Name == "First Avenue").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Garage" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Pantry" && l.Map.Name == "First Avenue").Id, Direction = "SW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Garage" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Pantry" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Downstairs Bathroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Kitchen" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Stairway Landing" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, Direction = "D" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Stairway Landing" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, Direction = "U" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Stairway Landing" && l.Map.Name == "First Avenue").Id, Direction = "D" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "First Avenue").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Guest Bathroom" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Guest Bedroom" && l.Map.Name == "First Avenue").Id, Direction = "SE" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Guest Bathroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Guest Bedroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, Direction = "NW" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Stairway Overlook" && l.Map.Name == "First Avenue").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Toilet" && l.Map.Name == "First Avenue").Id, Direction = "W" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Closet" && l.Map.Name == "First Avenue").Id, Direction = "S" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Toilet" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "First Avenue").Id, Direction = "E" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Closet" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "First Avenue").Id, Direction = "N" });
            connections.Add(new LocationConnection { FromLocationId = locations.Single(l => l.Name == "Master Bathroom" && l.Map.Name == "First Avenue").Id, ToLocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "First Avenue").Id, Direction = "N" });

            foreach (LocationConnection lc in connections)
            {
                context.LocationConnections.Add(lc);
            }
            context.SaveChanges();


            // Populate Item table
            List<Item> items = new List<Item>();

            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Bonus Room" && l.Map.Name == "Main Street").Id, Name = "exercise equipment", Description = "There are some dumbbells, a yoga mat, and a pull-up bar.", Plural = false, Determiner = "some" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, Name = "countertop island", Description = "It's a movable island with the same granite top as the kitchen countertops.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Breakfast Nook" && l.Map.Name == "Main Street").Id, Name = "high wooden chairs", Description = "Two tall bar chairs made of medium-brown wood.", Plural = true, Determiner = "two" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "Main Street").Id, Name = "dining room table", Description = "A long wooden table with a leaf in the middle.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "Main Street").Id, Name = "wooden chairs", Description = "Several different styles of wooden chairs.", Plural = true, Determiner = "some" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Garage" && l.Map.Name == "Main Street").Id, Name = "Ferrari", Description = "It's bright red, and looks very expensive and imaginary.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Garage" && l.Map.Name == "Main Street").Id, Name = "Porsche", Description = "It's purple, and looks very expensive and imaginary.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "Main Street").Id, Name = "dryer", Description = "A white, front-loading electric dryer.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "Main Street").Id, Name = "washer", Description = "A white, front-loading model.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Name = "black chair", Description = "The chair is made of black leather, and appears to be covered in small claw marks.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Name = "black couch", Description = "The couch is made of soft microfiber.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "Main Street").Id, Name = "television", Description = "An early-model LCD flat-screen.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Master Bedroom" && l.Map.Name == "Main Street").Id, Name = "book", Description = "'Game of Thrones', by George R.R. Martin.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "Main Street").Id, Name = "desk", Description = "The desk is of simple construction, made of black-laquered wood.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "Main Street").Id, Name = "desk chair", Description = "It's a basic swiveling office chair with a black cushion.", Plural = false, Determiner = "a" });

            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "First Avenue").Id, Name = "dining room table", Description = "A sturdy light blonde wooden table.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Dining Room" && l.Map.Name == "First Avenue").Id, Name = "wooden chairs", Description = "These are four light blonde wooden chairs.  Some have green cushions, and some have blue.", Plural = true, Determiner = "some" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "First Avenue").Id, Name = "dryer", Description = "A white, top-loading gas dryer.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Laundry Room" && l.Map.Name == "First Avenue").Id, Name = "washer", Description = "A white, top-loading washer.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, Name = "white chair", Description = "A white leather chair.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Living Room" && l.Map.Name == "First Avenue").Id, Name = "white couch", Description = "A white leather couch that appears to be covered in small claw marks.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, Name = "sofa", Description = "A comfy-looking plaid couch.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Loft" && l.Map.Name == "First Avenue").Id, Name = "television", Description = "The television is a huge, heavy-looking tube model.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "First Avenue").Id, Name = "desk", Description = "The desk is of simple construction, made of black - laquered wood.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Office" && l.Map.Name == "First Avenue").Id, Name = "desk chair", Description = "It's a basic swiveling office chair with a black cushion.", Plural = false, Determiner = "a" });
            items.Add(new Item { LocationId = locations.Single(l => l.Name == "Pantry" && l.Map.Name == "First Avenue").Id, Name = "can of soup", Description = "'Hearty Chicken Noodle'", Plural = false, Determiner = "a" });

            foreach (Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

        }

    }

}
