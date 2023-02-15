namespace rpg;
class Program
{
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
    }
}
