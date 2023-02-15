public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Item? ItemRequiredToEnter;
    public Quest? QuestAvailableHere;
    public Monster? MonsterLivingHere;
    public Location? LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;

    public Location(int id, string name, string description, Item? itemRequiredToEnter)
    {
      ID = id;
      Name = name;
      Description = description;
      ItemRequiredToEnter = itemRequiredToEnter;
    }
}
