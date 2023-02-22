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
        if (IsCompleted) return;

        foreach (var questCompletionItem in TheQuest.QuestCompletionItems.TheCountedItemList)
        {
            CountedItem? playerInvItem = player.Inventory.GetItemById(questCompletionItem.TheItem.ID);
            if (playerInvItem is null) return;

            if (!(playerInvItem.Quantity >= questCompletionItem.Quantity)) return;
            IsCompleted = true;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nYou completed the quest {TheQuest.Name}!");
            Console.ResetColor();


            Item? rewardItem = TheQuest.RewardItem;
            if (rewardItem is not null) player.Inventory.AddItem(rewardItem);

            Weapon? rewardWeapon = TheQuest.RewardWeapon;
            if (rewardWeapon is not null)
            {
                player.CurrentWeapon = rewardWeapon;
                Console.Write($"You have recieved and equipped a ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(rewardWeapon.Name);
                Console.ResetColor();
                Console.WriteLine(" as your new weapon.");

            };

            player.AddGold(TheQuest.RewardGold);
            player.AddExperience(TheQuest.RewardExperiencePoints);
        }
    }
}
