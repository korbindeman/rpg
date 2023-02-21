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
        Console.WriteLine("Type N to go to the Town Square. Or type HELP to see all available commands.");
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

        while (ThePlayer.CurrentHitPoints > 0 && CurrentMonster.CurrentHitPoints > 0)
        {
            int playerDamage = Convert.ToInt32(Math.Round(rnd.Next(weapon.MinimumDamage, weapon.MaximumDamage) * ThePlayer.AttackMultiplier));
            CurrentMonster.CurrentHitPoints -= playerDamage;
            Console.WriteLine($"You hit the {CurrentMonster.Name} for {playerDamage} health.");

            int monsterDamage = rnd.Next(weapon.MinimumDamage, weapon.MaximumDamage);
            ThePlayer.CurrentHitPoints -= monsterDamage;
            Console.WriteLine($"The {CurrentMonster.Name} hit you for {monsterDamage} health.");
        }
        bool playerWon = ThePlayer.CurrentHitPoints > 0;

        // TODO: refactor with guard clause
        if (playerWon)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"You have defeated the {CurrentMonster.Name}.\n");
            Console.ResetColor();

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
            Console.WriteLine($"You have been defeated by the {CurrentMonster.Name}.\n");
            Console.ResetColor();


            // TODO: maybe this should be in the Die method instead
            CountedItemList inv = ThePlayer.Inventory;
            if (inv.TheCountedItemList.Any())
            {
                int removedItemId = World.ITEM_ID_ADVENTURER_PASS;
                CountedItem? removedItem = null;
                while (removedItemId == World.ITEM_ID_ADVENTURER_PASS)
                {
                    int index = rnd.Next(inv.TheCountedItemList.Count);
                    removedItem = inv.TheCountedItemList[index];
                    removedItemId = removedItem.TheItem.ID;
                }
                if (removedItem is not null && removedItem.Quantity > 0)
                {
                    ThePlayer.Inventory.AddCountedItem(new CountedItem(removedItem.TheItem, -1));
                    Console.WriteLine($"You lost one {removedItem.TheItem.Name}");
                }
            }
            ThePlayer.Die();
        }
    }
}

