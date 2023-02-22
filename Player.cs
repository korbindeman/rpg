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
    public double AttackMultiplier;
    public int PointsToLevelUp = 20;

    public Player(string name)
    {
        Name = name;
        CurrentHitPoints = 20;
        MaximumHitPoints = 20;
        Gold = 10;
        ExperiencePoints = 0;
        Level = 1;
        CurrentWeapon = World.WeaponByID(1);
        CurrentLocation = World.LocationByID(1);
        QuestLog = new QuestList();
        Inventory = new CountedItemList(true);
        AttackMultiplier = 1.0;
    }

    public void ShowStats()
    {
        Console.WriteLine($"{Name}'s stats:");
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"XP: {ExperiencePoints}/{(Level + 1) * PointsToLevelUp} for next level");
        Console.WriteLine($"Weapon: {CurrentWeapon.Name} ({CurrentWeapon.MinimumDamage}-{CurrentWeapon.MaximumDamage} DMG)");
        Console.WriteLine($"Attack multiplier: {AttackMultiplier}");
        Console.WriteLine($"Gold: {Gold}");
    }

    public void AddGold(int gold)
    {
        Gold += gold;
        Console.Write($"You have received ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{gold} Gold");
        Console.ResetColor();
        Console.WriteLine(".");

    }

    public void AddExperience(int xp)
    {
        ExperiencePoints += xp;
        Console.Write($"You have gained ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{xp} XP");
        Console.ResetColor();
        Console.WriteLine(".");

        int oldLevel = Level;
        Level = ExperiencePoints / PointsToLevelUp + 1;
        int diff = Level - oldLevel;
        if (diff > 0)
        {
            MaximumHitPoints += 5 * diff;
            AttackMultiplier += 0.125 * diff;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nYou have leveled up to level {Level}! Your max HP is now {MaximumHitPoints}, and your attacks now do more damage.");
            Console.ResetColor();

        }
    }

    public void Die()
    {
        CurrentLocation = World.LocationByID(1);
        CurrentHitPoints = MaximumHitPoints;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("You passed out and woke up back at home with full health.\n");
        Console.ResetColor();
        if (Inventory.TheCountedItemList.Any())
        {
            int removedItemId = World.ITEM_ID_ADVENTURER_PASS;
            CountedItem? removedItem = null;
            while (removedItemId == World.ITEM_ID_ADVENTURER_PASS)
            {
                int index = new Random().Next(Inventory.TheCountedItemList.Count);
                removedItem = Inventory.TheCountedItemList[index];
                removedItemId = removedItem.TheItem.ID;
            }
            if (removedItem is not null && removedItem.Quantity > 0)
            {
                Inventory.AddCountedItem(new CountedItem(removedItem.TheItem, -1));
                Console.Write($"You lost one ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{removedItem.TheItem.Name}");
                Console.ResetColor();
                Console.Write($".\n");
            }
        }

    }

    public void GetQuest()
    {
        Quest? quest = CurrentLocation.QuestAvailableHere;
        if (quest is null)
        {
            Console.WriteLine("There is no quest availble here.");
            return;
        }
        foreach (var playerQuest in QuestLog.QuestLog)
        {
            if (playerQuest.TheQuest.ID == quest.ID)
            {
                Console.WriteLine("You already have this quest!");
                return;
            }
        }
        QuestLog.AddQuest(quest);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"Quest added: ");
        Console.ResetColor();
        Console.Write($"{quest.Name}\n");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(quest.Description);
        Console.ResetColor();
    }

    public void ViewInventory()
    {
        Console.WriteLine($"{Name}'s inventory:");

        bool isEmpty = true;

        foreach (var countedItem in Inventory.TheCountedItemList)
        {
            if (countedItem.Quantity == 0) continue;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{countedItem.Quantity}x ");
            Console.ResetColor();
            string name = countedItem.Quantity == 1 ? countedItem.TheItem.Name : countedItem.TheItem.NamePlural;
            Console.WriteLine($"- {name}");

            isEmpty = false;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        if (isEmpty) Console.WriteLine("Your inventory is empty.");
        Console.ResetColor();
    }

    public void ViewQuests()
    {
        // TODO: should probably be part of the Quest class
        Console.WriteLine($"{Name}'s quests:");
        foreach (var playerQuest in QuestLog.QuestLog)
        {
            string isCompleted = playerQuest.IsCompleted ? "Yes" : "No";
            Console.Write($"{playerQuest.TheQuest.Name}:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {playerQuest.TheQuest.Description}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" (completed: {isCompleted})\n");
            Console.ResetColor();

        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        if (!QuestLog.QuestLog.Any()) Console.WriteLine("You don't have any quests.");
        Console.ResetColor();
    }

    public void CheckHealth()
    {
        if (CurrentHitPoints < 10)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nYou have {CurrentHitPoints} HP remaining. Maybe you should heal.");
            Console.ResetColor();
        }
    }

    public void Heal()
    {
        if (CurrentHitPoints == MaximumHitPoints)
        {
            Console.WriteLine("You are already fully healed.");

            return;
        }
        int oldHP = CurrentHitPoints;

        int heal = new Random().Next(MaximumHitPoints / 3, MaximumHitPoints / 2);
        // CurrentHitPoints = Math.Min(CurrentHitPoints + heal, MaximumHitPoints);
        CurrentHitPoints = MaximumHitPoints;

        int deltaHP = CurrentHitPoints - oldHP;
        Console.Write($"You gained {deltaHP} HP. Now you have ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{CurrentHitPoints} HP");
        Console.ResetColor();
        Console.Write(" again.\n");
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
        if (targetLocation is null)
        {
            Console.WriteLine("There is nothing there.");
            return;
        }

        if (targetLocation.ID == 3 && Inventory.GetItemById(7) is null)
        {
            Console.Write("You can't go here without an ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"Adventurer Pass");
            Console.ResetColor();
            Console.WriteLine("! Come back when you have one.");

            return;
        }

        CurrentLocation = targetLocation ?? CurrentLocation;

        string questMark = CurrentLocation.QuestAvailableHere is not null && !QuestLog.QuestLog.Exists(playerQuest => playerQuest.TheQuest.ID == CurrentLocation.QuestAvailableHere.ID) ? " (quest!)" : "";

        Console.Write($"Moved to {CurrentLocation.Name}");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(questMark);
        Console.ResetColor();
        Console.WriteLine(".");
    }

    public void CheckWin()
    {
        if (Inventory.GetItemById(8) is null) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nCONGRATULATIONS {Name}, YOU HAVE WON THE GAME!");
        Console.ResetColor();
    }
}
