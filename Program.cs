namespace rpg;
class Program
{
    static SuperAdventure adventure;

    static void Main(string[] args)
    {
        adventure = new SuperAdventure();
        while (true)
        {
            Console.Write("> ");
            string? command = Console.ReadLine();
            ProcessCommand(command);
            Console.Write("\n");
        }
    }

    static void ProcessCommand(string? command)
    {
        Player p = adventure.ThePlayer;
        p.Inventory.AddItem(World.ItemByID(1));

        switch (command)
        {
            case "n":
                p.Move("north");
                Console.WriteLine("Moved to {0}", p.CurrentLocation.Name);
                break;
            case "e":
                p.Move("east");
                Console.WriteLine("Moved to {0}", p.CurrentLocation.Name);
                break;
            case "s":
                p.Move("south");
                Console.WriteLine("Moved to {0}", p.CurrentLocation.Name);
                break;
            case "w":
                p.Move("west");
                Console.WriteLine("Moved to {0}", p.CurrentLocation.Name);
                break;
            case "i":
                p.ViewInventory();
                break;
            case "map":
                p.CurrentLocation.ShowMap();
                break;
            case "heal":
                p.Heal(2);
                Console.WriteLine(p.CurrentHitPoints);
                break;
            case "describe":
                Console.WriteLine(p.CurrentLocation.Description);
                break;
            case "quit":
                Environment.Exit(0);
                break;
        }
    }
}
