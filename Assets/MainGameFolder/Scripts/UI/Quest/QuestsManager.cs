using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestsManager : MonoBehaviour
    {
        private readonly Dictionary<int, string[]> _quests = new();
        
        private QuestListManager _questListManager;

        private void Start()
        {
            _questListManager = GetComponent<QuestListManager>();
            _quests.Add(1, new[] {"12321"});
            SetupQuestsByScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void SetupQuestsByScene(int sceneNumber)
        {
            var quests = _quests.GetValueOrDefault(sceneNumber);
            if (quests == null)
                throw new Exception("Scene not found");
            Debug.Log(quests);
            _questListManager.SetupQuests(quests);
            _questListManager.ShowQuestUI();
        }
    }
}