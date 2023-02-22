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

    public void ShowMap(Player player)
    {
        // TODO: needs refactoring
        Console.WriteLine($"You are now at {Name}.");
        Console.WriteLine($"{Description}\n");

        Location empty = new Location(0, "Nothing", "", null);

        string[] directions = { "N", "E", "S", "W" };
        Location?[] directionLocations = { LocationToNorth, LocationToEast, LocationToSouth, LocationToWest };
        var directionsAndLocations = directions.Zip(directionLocations, (d, l) => new { Direction = d, Location = l });


        ConsoleColor[] colors = { ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Magenta };
        int index = 0;

        foreach (var dl in directionsAndLocations)
        {
            Location? loc = dl.Location;
            loc ??= empty;
            Console.Write($"{dl.Direction}: ");

            string questMark = loc.QuestAvailableHere is not null && !player.QuestLog.QuestLog.Exists(playerQuest => playerQuest.TheQuest.ID == loc.QuestAvailableHere.ID) ? " (!)" : "";
            string enemyMark = loc.MonsterLivingHere is not null ? " (!)" : "";

            Console.ForegroundColor = loc.Name == "Nothing" ? ConsoleColor.DarkGray : colors[index];
            Console.Write(loc.Name);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(questMark);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(enemyMark);
            Console.ResetColor();
            Console.WriteLine();

            index++;
        }
    }
}
