namespace rpg;
class Program
{
    static SuperAdventure adventure = new SuperAdventure();

    static void Main(string[] args)
    {
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
            case "x":
                adventure.ThePlayer.CurrentLocation.ShowMap();
                break;
            case "quest":
                adventure.ThePlayer.GetQuest();
                break;
            case "quests":
                adventure.ThePlayer.ViewQuests();
                break;
            case "map":
                adventure.ThePlayer.CurrentLocation.ShowMap();
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
            Console.Write("---- Help Menu: all commands are case-insensitive ----");
            Console.ResetColor();
            Console.WriteLine(@"
N, E, S, W                                 Move around
I                                       Open inventory
X, MAP                                        Open map
QUEST                                        Get quest
QUESTS                                 View your quest
FIGHT                                   Fight an enemy
HEAL                                     Heal yourself
STATS                                  View your stats
QUIT                                     Quit the game");
        }
    }
}
