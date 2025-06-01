using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestListManager : MonoBehaviour
    {
        [SerializeField] private Canvas questCanvas;

        private Dictionary<string, bool> _quests;

        public void SetupQuests(string[] quests)
        {
            _quests = quests.ToDictionary(quest => quest, _ => false);
            CreateQuestText(quests[0]);
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
            _quests.Clear();
        }

        public void MarkQuestDone(string questName)
        {
            _quests[questName] = true;
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