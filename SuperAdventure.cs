class SuperAdventure
{
    public Player ThePlayer;
    public Monster? CurrentMonster;

    public SuperAdventure()
    {
        // Console.WriteLine("Hello, what is your name?");
        // string name = Console.ReadLine();
        string name = "Korbin"; // temporary for development
        ThePlayer = new Player(name);

        CurrentMonster = null;
    }

    public void Fight()
    {
        CurrentMonster = ThePlayer.CurrentLocation.MonsterLivingHere;
        if (CurrentMonster is null)
        {
            Console.WriteLine("There are no monsters in this location.");
            return;
        }
        else
        {
            Console.WriteLine($"Fighting {CurrentMonster.Name}.");
            // TODO: implement fighting mechanics

            foreach (var countedItem in CurrentMonster.Loot.TheCountedItemList)
            {
                ThePlayer.Inventory.AddCountedItem(countedItem);
            }
        }
    }
}
