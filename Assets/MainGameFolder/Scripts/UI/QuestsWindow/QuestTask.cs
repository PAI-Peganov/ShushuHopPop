using UnityEngine;

namespace MainGameFolder.Scripts.UI.QuestsWindow
{
    public class QuestTask : MonoBehaviour
    {
        [SerializeField] private string questText;
        public string QuestText => questText;

        [SerializeField] private string questName;
        public string Name => questName;

        [SerializeField] private int order;
        public int Order => order;

        [SerializeField] private QuestTask previousQuestTask;
        public QuestTask PreviousQuestTask => previousQuestTask;

        [SerializeField] private int progress;
        public int Progress => progress;
        
        public bool IsDone { get; private set; }

        public void MakeDone()
        {
            progress--;
            if (progress <= 0)
                IsDone = true;
        }
    }
}