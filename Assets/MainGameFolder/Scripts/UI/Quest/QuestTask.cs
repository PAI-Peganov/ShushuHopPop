namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestTask
    {
        public string QuestText { get; private set; }
        
        public int Order { get; private set; }
        public bool IsDone { get; private set; }

        private float progres = 0f;

        public QuestTask(string questText, int order)
        {
            QuestText = questText;
            Order = order;
        }

        public void MakeDone()
        {
            IsDone = true;
        }

        public void UpdateProgres()
        {

        }
    }
}