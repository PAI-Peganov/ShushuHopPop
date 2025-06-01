using System.Collections.Generic;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class Quest
    {
        public string QuestName { get; private set; }
        public QuestTask[] Quests { get; private set; }

        public Quest(string questName, List<QuestTask> quests)
        {
            QuestName = questName;
            Quests = quests.ToArray();
        }
    }
}