namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestTask
    {
        private string questText;
        private bool isDone;

        public QuestTask(string questText)
        {
            this.questText = questText;
        }

        public void MakeDone()
        {
            isDone = true;
        }
    }
}