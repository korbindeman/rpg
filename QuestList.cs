public class QuestList
{
    public List<PlayerQuest> QuestLog;

    public QuestList()
    {
        QuestLog = new List<PlayerQuest>();
    }

    public void AddQuest(Quest quest)
    {
        QuestLog.Add(new PlayerQuest(quest));
    }

    public void CheckQuestCompletion(Player player)
    {
        foreach (var playerQuest in player.QuestLog.QuestLog)
        {
            playerQuest.CheckCompletion(player);
        }
    }
}
