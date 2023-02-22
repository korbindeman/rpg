namespace rpg;
class Program
{
    static SuperAdventure adventure = new SuperAdventure();

    static void Main(string[] args)
    {
        string intro = @"
WELCOME TO 'Monster Hunter'!

As the hero of your town, you must clear the town and its surroundings
of all the deadly monsters like snakes, rats and spiders and complete
challenging quests and collect powerful weapons to emerge victorious.

The fate of the town is in your hands!

";
        Typewrite(intro);

        Player p = adventure.ThePlayer;

        while (true)
        {
            Console.Write("\n> ");
            string? command = Console.ReadLine();
            if (command is null) command = "";
            ProcessCommand(command);
            p.QuestLog.CheckQuestCompletion(p);
            p.CheckWin();
        }
    }

    static void Typewrite(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];
            Console.Write(c);
            System.Threading.Thread.Sleep(40);
            if (c == ',') System.Threading.Thread.Sleep(50);
            if (c == '.' || c == '!') System.Threading.Thread.Sleep(100);
        }

    }

    static void ProcessCommand(string command)
    {
        command = command.ToLower();
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
            case "quest":
                adventure.ThePlayer.GetQuest();
                break;
            case "quests":
                adventure.ThePlayer.ViewQuests();
                break;
            case "map":
                adventure.ThePlayer.CurrentLocation.ShowMap(adventure.ThePlayer);
                break;
            case "heal":
                adventure.ThePlayer.Heal();
                break;
            case "fight":
                adventure.Fight();
                break;
            case "stats":
                adventure.ThePlayer.ShowStats();
                break;
            case "quit":
                Environment.Exit(0);
                break;
            case "help":
                Help();
                break;
            default:

                Console.WriteLine("That command is not recognized. Type HELP for a list of commands.");
                break;
        }

        static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("---- Help Menu: (all commands are case-insensitive) ----");
            Console.ResetColor();
            Console.WriteLine(@"
N, E, S, W                                   Move around
I                                    Open your inventory
MAP                       Open a map of the surroundings
QUEST                   Get the current location's quest
QUESTS                           View your active quests
FIGHT                                     Fight an enemy
HEAL                                       Heal yourself
STATS                                    View your stats
QUIT                                       Quit the game");
        }
    }
}
