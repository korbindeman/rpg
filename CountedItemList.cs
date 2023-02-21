public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;
    private bool Echo;

    public CountedItemList(bool echo = false)
    {
        TheCountedItemList = new List<CountedItem>();
        Echo = echo;
    }

    public void AddItem(Item item)
    {
        AddCountedItem(new CountedItem(item, 1));
    }

    public void AddCountedItemList(CountedItemList countedItemList)
    {
        foreach (var countedItem in countedItemList.TheCountedItemList)
        {
            AddCountedItem(countedItem);
        }
    }

    public void AddCountedItem(CountedItem countedItem)
    {
        string name = countedItem.Quantity == 1 ? countedItem.TheItem.Name : countedItem.TheItem.NamePlural;
        if (Echo && countedItem.Quantity > 0) Console.WriteLine($"{countedItem.Quantity} {name} added to your inventory.");

        foreach (var inventoryCountedItem in TheCountedItemList)
        {
            if (inventoryCountedItem.TheItem.ID == countedItem.TheItem.ID)
            {
                inventoryCountedItem.Quantity += countedItem.Quantity;
                if (inventoryCountedItem.Quantity < 0) inventoryCountedItem.Quantity += -countedItem.Quantity;
                return;
            }
        }
        TheCountedItemList.Add(new CountedItem(countedItem.TheItem, countedItem.Quantity));
    }

    public CountedItem? GetItemById(int id)
    {
        foreach (var countedItem in TheCountedItemList)
        {
            if (countedItem.TheItem.ID == id)
            {
                return countedItem;
            }
        }
        return null;
    }
}

