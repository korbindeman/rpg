public class PlayerQuest
{
    public Quest TheQuest;
    public bool IsCompleted;

    public PlayerQuest(Quest quest)
    {
        TheQuest = quest;
        IsCompleted = false;
    }

    public void CheckCompletion(Player player)
    {
        // TODO: refactor
        foreach (var questCompletionItem in TheQuest.QuestCompletionItems.TheCountedItemList)
        {
            CountedItem? playerInvItem = player.Inventory.GetItemById(questCompletionItem.TheItem.ID);
            if (playerInvItem is not null)
            {
                if (playerInvItem.Quantity == questCompletionItem.Quantity)
                {
                    IsCompleted = true;
                }
                else
                {
                    IsCompleted = false;
                }
            }
        }
    }
}
