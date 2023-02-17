public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Item? ItemRequiredToEnter;
    public Quest? QuestAvailableHere;
    public Monster? MonsterLivingHere;
    public Location? LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;

    public Location(int id, string name, string description, Item? itemRequiredToEnter)
    {
        ID = id;
        Name = name;
        Description = description;
        ItemRequiredToEnter = itemRequiredToEnter;
    }

    public void ShowMap()
    {
        // TODO: needs refactoring
        Console.WriteLine($"You are now at {Name}.");
        Console.WriteLine($"{Description}");

        Location empty = new Location(0, "Nothing", "", null);

        string[] directions = { "North", "East", "South", "West" };
        Location?[] directionLocations = { LocationToNorth, LocationToEast, LocationToSouth, LocationToWest };
        var directionsAndLocations = directions.Zip(directionLocations, (d, l) => new { Direction = d, Location = l });

        foreach (var dl in directionsAndLocations)
        {
            Location? loc = dl.Location;
            loc ??= empty;
            Console.WriteLine($"{dl.Direction}: {loc.Name}");
        }
    }
}
