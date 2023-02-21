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
        Console.WriteLine($"You have received {gold} gold.");
    }

    public void AddExperience(int xp)
    {
        ExperiencePoints += xp;
        Console.WriteLine($"\nYou have gained {xp} XP.");
        int oldLevel = Level;
        Level = ExperiencePoints / PointsToLevelUp + 1;
        int diff = Level - oldLevel;
        if (diff > 0)
        {
            MaximumHitPoints += 5 * diff;
            AttackMultiplier += 0.125 * diff;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"You have leveled up to level {Level}! Your max HP is now {MaximumHitPoints}, and your attacks now do more damage.");
            Console.ResetColor();

        }
    }

    public void Die()
    {
        CurrentLocation = World.LocationByID(1);
        CurrentHitPoints = MaximumHitPoints;
        Console.WriteLine("\nYou passed out and woke up back at home with full health.");
    }

    public void GetQuest()
    {
        // TODO: should maybe be part of the Quest class
        Quest? quest = CurrentLocation.QuestAvailableHere;
        if (quest is not null)
        {
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
            Console.WriteLine(quest.Description);
        }
    }

    public void ViewInventory()
    {
        Console.WriteLine($"{Name}'s inventory:");
        foreach (var countedItem in Inventory.TheCountedItemList)
        {
            if (countedItem.Quantity == 0) continue;
            Console.WriteLine($"{countedItem.Quantity}x - {countedItem.TheItem.Name}");
        }
    }

    public void ViewQuests()
    {
        // TODO: should probably be part of the Quest class
        Console.WriteLine($"{Name}'s quests:");
        foreach (var playerQuest in QuestLog.QuestLog)
        {
            string isCompleted = playerQuest.IsCompleted ? "Yes" : "No";
            Console.WriteLine($"{playerQuest.TheQuest.Name}: {playerQuest.TheQuest.Description} (completed: {isCompleted})");
        }
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

        int heal = new Random().Next(MaximumHitPoints / 4, MaximumHitPoints / 2);
        CurrentHitPoints = Math.Min(CurrentHitPoints + heal, MaximumHitPoints);

        int deltaHP = CurrentHitPoints - oldHP;
        Console.Write($"You healed yourself and gained {deltaHP} points of health. Now you have ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{CurrentHitPoints} HP\n");
        Console.ResetColor();
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
            Console.WriteLine("You can't go here without an Adventurer Pass! Come back when you have one.");
            return;
        }

        CurrentLocation = targetLocation ?? CurrentLocation;

        string questMark = CurrentLocation.QuestAvailableHere is not null && !QuestLog.QuestLog.Exists(playerQuest => playerQuest.TheQuest.ID == CurrentLocation.QuestAvailableHere.ID) ? " (quest!)" : "";

        Console.Write($"Moved to {CurrentLocation.Name}");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(questMark);
        Console.ResetColor();
        Console.WriteLine();
    }

    public void CheckWin()
    {
        if (Inventory.GetItemById(8) is null) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nCONGRATULATIONS {Name}, YOU HAVE WON THE GAME!");
        Console.ResetColor();
    }
}
