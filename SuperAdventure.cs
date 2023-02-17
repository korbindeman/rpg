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
}
