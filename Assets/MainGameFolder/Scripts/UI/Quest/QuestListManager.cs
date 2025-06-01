using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestUIManager
    {
        [SerializeField] private Canvas _questCanvas;

        private Dictionary<string, bool> _quests;

        public void SetupQuests(List<string> quests)
        {
            _quests = quests.ToDictionary(quest => quest, _ => false);
        }

        public void ClearQuests()
        {
            _quests.Clear();
        }

        public void MarkQuestDone(string questName)
        {
            _quests[questName] = true;
        }

        public void ShowQuestUI()
        {
            _questCanvas.gameObject.SetActive(true);
        }

        public void HideQuestUI()
        {
            _questCanvas.gameObject.SetActive(false);
        }
    }
}