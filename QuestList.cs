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
}
