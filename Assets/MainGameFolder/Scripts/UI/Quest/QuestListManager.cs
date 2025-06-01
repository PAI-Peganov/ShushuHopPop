using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestListManager : MonoBehaviour
    {
        [SerializeField] private Canvas questCanvas;

        private Dictionary<string, QuestTask> quests;

        private void Start()
        {
            HideQuestUI();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                HideQuestUI();
            if (Input.GetKeyDown(KeyCode.Q))
                ShowQuestUI();
        }

        public void SetupQuests(QuestTask[] quests)
        {
            this.quests = quests.ToDictionary(quest => quest.key, quest => quest);
            CreateQuestText(quests[0].QuestText);
        }

        private void CreateQuestText(string questName)
        {
            var textObj = new GameObject(questName);
            textObj.transform.SetParent(questCanvas.transform, false);


            var text = textObj.AddComponent<Text>();
            text.text = "Привет, Unity!";
            text.fontSize = 32;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;

            var rectTransform = text.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 0); // центр экрана
            rectTransform.sizeDelta = new Vector2(400, 100); // ширина и высота

            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        public void ClearQuests()
        {
            quests.Clear();
        }

        public void MarkQuestDone(string questName)
        {
            //
        }

        public void ShowQuestUI()
        {
            questCanvas.gameObject.SetActive(true);
        }

        public void HideQuestUI()
        {
            questCanvas.gameObject.SetActive(false);
        }
    }
}