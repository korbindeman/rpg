public class Weapon
{
    public int ID;
    public string Name;
    public string NamePlural;
    public int MinimumDamage;
    public int MaximumDamage;

    public Weapon(int id, string name, string namePlural, int minimumDamage, int maximumDamage)
    {
        ID = id;
        Name = name;
        NamePlural = namePlural;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }
}
