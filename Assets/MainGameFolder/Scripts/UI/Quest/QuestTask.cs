namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestTask
    {
        public string QuestText { get; private set; }
        
        public string key { get; private set; }
        public bool IsDone { get; private set; }

        public QuestTask(string questText, string key)
        {
            QuestText = questText;
        }

        public void MakeDone()
        {
            IsDone = true;
        }
    }
}