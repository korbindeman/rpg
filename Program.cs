namespace rpg;
class Program
{
    static Player p = new Player("Korbin");

    static void Main(string[] args)
    {

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
        Console.WriteLine("command {0}", command);
        if (command == "n")
        {
            Location? newLocation = p.Move("north");
            Console.WriteLine("Moved to {0}", newLocation.Name);
        }
        if (command == "e")
        {
            Location? newLocation = p.Move("east`");
            Console.WriteLine("Moved to {0}", newLocation.Name);
        }
        if (command == "s")
        {
            Location? newLocation = p.Move("south");
            Console.WriteLine("Moved to {0}", newLocation.Name);
        }
        if (command == "w")
        {
            Location? newLocation = p.Move("west");
            Console.WriteLine("Moved to {0}", newLocation.Name);
        }
        if (command == "quit")
        {
            Environment.Exit(0);
        }
    }
}
