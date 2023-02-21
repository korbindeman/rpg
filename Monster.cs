public class Monster : ICloneable
{
    public int ID;
    public string Name;
    public string NamePlural;
    public int MaximumDamage;
    public int RewardExperience;
    public int RewardGold;
    public CountedItemList Loot;
    public int CurrentHitPoints;

    public Monster(int id, string name, string namePlural, int maximumDamage, int rewardExperience, int rewardGold, int currentHitPoints)
    {
        ID = id;
        Name = name;
        NamePlural = namePlural;
        MaximumDamage = maximumDamage;
        RewardExperience = rewardExperience;
        RewardGold = rewardGold;
        RewardGold = rewardGold;
        CurrentHitPoints = currentHitPoints;
        Loot = new CountedItemList();
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
