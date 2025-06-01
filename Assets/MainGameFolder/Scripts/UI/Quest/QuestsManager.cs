using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestsManager : MonoBehaviour
    {
        private readonly Dictionary<int, Quest> _quests = new Dictionary<int, Quest>
        {
            {
                1, new Quest(
                    "Переплыть реку",
                    new List<QuestTask>
                    {
                        new QuestTask("Найдите способ выбраться из заточения в ловушке болот – время на исходе", 1),
                        new QuestTask("Немедленно покиньте болота, прежде чем они окончательно поглотят вас.", 2)
                    }
                )
            },
            {
                2, new Quest(
                    "Найти ключ",
                    new List<QuestTask>
                    {
                        new QuestTask("Найдите способ выбраться из заточения в ловушке болот – время на исходе", 1),
                        new QuestTask("Немедленно покиньте болота, прежде чем они окончательно поглотят вас.", 2)
                    }
                )
            },
            {
                3, new Quest(
                    "Найти вакцину",
                    new List<QuestTask>
                    {
                        new QuestTask("Найдите способ выбраться из заточения в ловушке болот – время на исходе", 1),
                        new QuestTask("Немедленно покиньте болота, прежде чем они окончательно поглотят вас.", 2)
                    }
                )
            }
        };

        [SerializeField] private QuestListManager questListManager;

        private void Start()
        {
            SetupQuestsByScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void SetupQuestsByScene(int sceneNumber)
        {
            var quest = _quests.GetValueOrDefault(sceneNumber);
            if (quest == null)
                throw new Exception("Scene not found");
            questListManager.SetupQuests(quest.Quests);
        }
    }
}