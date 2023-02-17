public class Player
{
    public string Name;
    public int CurrentHitPoints;
    public int MaximumHitPoints;
    public int Gold;
    public int ExperiencePoints;
    public int Level;
    public Weapon CurrentWeapon;
    public Location CurrentLocation;
    public QuestList QuestLog;
    public CountedItemList Inventory;

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
        QuestLog = new QuestList();
        Inventory = new CountedItemList();
    }

    public void GetQuest()
    {
        Quest? quest = CurrentLocation.QuestAvailableHere;
        if (quest is not null)
        {
            QuestLog.AddQuest(quest);
            Console.WriteLine($"Quest added: {quest.Name}");
        }
    }

    public void ViewInventory()
    {
        // TODO: should probably be part of the Inventory class
        Console.WriteLine($"{Name}'s inventory:");
        foreach (var countedItem in Inventory.TheCountedItemList)
        {
            Console.WriteLine($"{countedItem.Quantity}x - {countedItem.TheItem.Name}");
        }
    }

    public void ViewQuests()
    {
        Console.WriteLine($"{Name}'s quests:");
        foreach (var playerQuest in QuestLog.QuestLog)
        {
            string isCompleted = playerQuest.IsCompleted ? "Yes" : "No";
            Console.WriteLine($"{playerQuest.TheQuest.Name}: {playerQuest.TheQuest.Description} (completed: {isCompleted})");
        }
    }


    public void Heal(int hp)
    {
        CurrentHitPoints = Math.Min(CurrentHitPoints + hp, MaximumHitPoints);
        Console.WriteLine($"You healed yourself and gained {hp} points of health.");
    }

    public void Move(string direction)
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
        CurrentLocation = targetLocation ?? CurrentLocation;
    }
}
