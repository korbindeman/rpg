public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;

    public CountedItemList()
    {
        TheCountedItemList = new List<CountedItem>();
    }

    public void AddItem(Item item)
    {
        TheCountedItemList.Add(new CountedItem(item, 1));
    }

    public void AddCountedItem(CountedItem countedItem)
    {
        TheCountedItemList.Add(countedItem);
    }
}

