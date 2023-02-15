class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public int Gold;
    public int ExperiencePoints;
    public int Level;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;
    // public QuestList QuestLog;
    // public Inventory CountedItemList;

    public Player(string name)
    {
        Name = name;
        CurrentHitPoints = 10;
        MaximumHitPoints = 20;
        Gold = 10;
        ExperiencePoints = 0;
        Level = 1;
        CurrentWeapon = World.WeaponByID(1);
        CurrentLocation = World.LocationByID(1);
    }

    public Location? Move(string direction)
    {
        Location? targetLocation = null;
        switch (direction)
        {
            case "north":
                targetLocation = CurrentLocation.LocationToNorth;
                break;
            case "east":
                targetLocation = CurrentLocation.LocationToEast;
                break;
            case "south":
                targetLocation = CurrentLocation.LocationToSouth;
                break;
            case "west":
                targetLocation = CurrentLocation.LocationToWest;
                break;
        }
        if (targetLocation is null) return null;

        CurrentLocation = targetLocation;

        return targetLocation;
    }
}
