public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;

    public CountedItemList()
    {
        TheCountedItemList = new List<CountedItem>();
    }

    public void AddItem(Item item)
    {
        foreach (var countedItem in TheCountedItemList)
        {
            if (countedItem.TheItem.ID == item.ID)
            {
                countedItem.Quantity++;
                return;
            }
        }
        TheCountedItemList.Add(new CountedItem(item, 1));
    }

    public void AddCountedItem(CountedItem countedItem)
    {
        for (int i = 0; i < countedItem.Quantity; i++)
        {
            AddItem(countedItem.TheItem);
        }
    }
}

