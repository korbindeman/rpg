namespace rpg;
class Program
{
    static SuperAdventure adventure;

    static void Main(string[] args)
    {
        adventure = new SuperAdventure();

        // TEMPORARY
        Player p = adventure.ThePlayer;
        adventure.ThePlayer.Inventory.AddItem(World.ItemByID(1));
        // END TEMPORARY

        while (true)
        {
            Console.Write("> ");
            string? command = Console.ReadLine();
            ProcessCommand(command);
            p.QuestLog.CheckQuestCompletion(p);
        }
    }

    static void ProcessCommand(string? command)
    {
        switch (command)
        {
            case "n":
                adventure.ThePlayer.Move("north");
                break;
            case "e":
                adventure.ThePlayer.Move("east");
                break;
            case "s":
                adventure.ThePlayer.Move("south");
                break;
            case "w":
                adventure.ThePlayer.Move("west");
                break;
            case "i":
                adventure.ThePlayer.ViewInventory();
                break;
            case "quests":
                adventure.ThePlayer.ViewQuests();
                break;
            case "quest":
                adventure.ThePlayer.GetQuest();
                break;
            case "map":
                adventure.ThePlayer.CurrentLocation.ShowMap();
                break;
            case "heal":
                adventure.ThePlayer.Heal(2);
                Console.WriteLine(adventure.ThePlayer.CurrentHitPoints);
                break;
            case "describe":
                Console.WriteLine(adventure.ThePlayer.CurrentLocation.Description);
                break;
            case "fight":
                adventure.Fight();
                break;
            case "quit":
                Environment.Exit(0);
                break;
        }
    }
}
