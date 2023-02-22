class SuperAdventure
{
    public Player ThePlayer;
    public Monster? CurrentMonster;

    public SuperAdventure()
    {
        Console.WriteLine("Hello adventurer, what is your name?");
        Console.Write("> ");
        string? name = Console.ReadLine();
        if (name is null || name == "") name = "Adventurer";
        ThePlayer = new Player(name);
        Console.WriteLine();

        CurrentMonster = null;

        Console.WriteLine("You wake up and get up from your bed.");
        Console.Write("Type ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("N");
        Console.ResetColor();
        Console.Write(" to go to the Town Square. Or type ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("HELP");
        Console.ResetColor();
        Console.Write(" to go to the Town Square. Or type HELP to see all available commands.\n");
    }

    public void Fight()
    {
        if (ThePlayer.CurrentLocation.MonsterLivingHere is null)
        {
            Console.WriteLine("There are no monsters in this location.");
            return;
        }
        CurrentMonster = (Monster)ThePlayer.CurrentLocation.MonsterLivingHere.Clone();

        Console.WriteLine($"Fighting {CurrentMonster.Name}.\n");

        Random rnd = new Random();

        Weapon weapon = ThePlayer.CurrentWeapon;

        int turns = 0;
        int damageTaken = 0;
        while (ThePlayer.CurrentHitPoints > 0 && CurrentMonster.CurrentHitPoints > 0)
        {
            int playerDamage = Convert.ToInt32(Math.Round(rnd.Next(weapon.MinimumDamage, weapon.MaximumDamage) * ThePlayer.AttackMultiplier));
            CurrentMonster.CurrentHitPoints -= playerDamage;
            // Console.WriteLine($"You hit the {CurrentMonster.Name} for {playerDamage} HP.");

            int monsterDamage = rnd.Next(weapon.MinimumDamage, weapon.MaximumDamage);
            ThePlayer.CurrentHitPoints -= monsterDamage;
            // Console.WriteLine($"The {CurrentMonster.Name} hit you for {monsterDamage} HP.");
            turns++;
            damageTaken += monsterDamage;
        }
        bool playerWon = ThePlayer.CurrentHitPoints > 0;

        // TODO: refactor with guard clause
        if (playerWon)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"You have defeated the {CurrentMonster.Name}.");
            Console.ResetColor();

            string swing = turns == 1 ? "only one swing" : $"{turns} swings";

            Console.Write($"It took you ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(swing);
            Console.ResetColor();
            Console.Write(", and you suffered ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{damageTaken} HP");
            Console.ResetColor();
            Console.Write(" of damage.\n\n");

            CountedItemList loot = CurrentMonster.Loot;
            int index = rnd.Next(loot.TheCountedItemList.Count);
            ThePlayer.Inventory.AddCountedItem(loot.TheCountedItemList[index]);

            ThePlayer.AddGold(CurrentMonster.RewardGold);
            ThePlayer.AddExperience(CurrentMonster.RewardExperience);

            ThePlayer.CheckHealth();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You have been defeated by the {CurrentMonster.Name}.");
            Console.ResetColor();

            ThePlayer.Die();
        }
    }
}

