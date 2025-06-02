using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainGameFolder.Scripts.UI.Quest
{
    public class QuestListManager : MonoBehaviour
    {
        [SerializeField] private Canvas questCanvas;
        [SerializeField] string nextSceneName;

        private readonly Dictionary<int, (QuestTask task, GameObject gameObject)> _quests = new();

        private bool _isShown;

        private QuestTask _currentQuest;

        private void Start()
        {
            HideQuestUI();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isShown = false;
                HideQuestUI();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_isShown)
                {
                    _isShown = false;
                    HideQuestUI();
                }
                else
                {
                    _isShown = true;
                    ShowQuestUI();
                }
            }
        }

        private void FixedUpdate()
        {
            
        }

        private static string InsertLineBreaks(string input, int maxLineLength = 50)
        {
            var lastBreak = 0;
            while (lastBreak + maxLineLength < input.Length)
            {
                var breakIndex = input.LastIndexOf(' ', lastBreak + maxLineLength, maxLineLength);
                if (breakIndex == -1) break;

                input = input.Remove(breakIndex, 1).Insert(breakIndex, "\n");
                lastBreak = breakIndex + 1;
            }

            return input;
        }

        private static string Strikethrough(string input)
        {
            const char strikethroughChar = '\u0336';
            return string.Concat(input.Select(c => $"{c}{strikethroughChar}"));
        }

        public void SetupQuests(QuestTask[] quests)
        {
            for (var i = 0; i < quests.Length; i++)
                CreateQuestText(quests[i], new Vector2(0, -125 * i), i == 0);
            _currentQuest = quests[0];
        }

        private void CreateQuestText(QuestTask quest, Vector2 offset, bool isVisible = true)
        {
            var textObj = new GameObject($"Quest number {quest.Order}");
            textObj.transform.SetParent(questCanvas.transform, false);
            if (!isVisible)
                textObj.SetActive(false);


            var text = textObj.AddComponent<Text>();
            text.text = InsertLineBreaks(quest.QuestText);
            text.fontSize = 40;
            text.color = Color.black;
            text.alignment = TextAnchor.UpperLeft;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.font = Resources.Load<Font>("UI/Fonts/static/Montserrat-Bold");

            var rectTransform = text.GetComponent<RectTransform>();
            // Смещение от левого верхнего угла
            rectTransform.anchoredPosition = new Vector2(-400, 300) + offset;
            // Text size
            rectTransform.sizeDelta = new Vector2(400, 50);

            _quests.Add(quest.Order, (quest, textObj));
        }

        private bool TryShowNextQuest()
        {
            if (_currentQuest is null)
                return false;
            if (!_quests.TryGetValue(_currentQuest.Order + 1, out var quest))
            {
                return false;
                throw new Exception("Quest not found: " + _currentQuest.Order + 1);
            }
            quest.gameObject.SetActive(true);
            _currentQuest = quest.task;
            return true;
        }


        public void ClearQuests()
        {
            foreach (var quest in _quests.Values)
                Destroy(quest.gameObject);
            _quests.Clear();
        }

        public bool TryMarkQuestAsCompleted(int order)
        {
            if (_currentQuest.Order != order)
                return false;
            if (!_quests.TryGetValue(order, out var quest))
                throw new Exception("Quest not found: " + order);
            quest.task.MakeDone();
            var text = quest.gameObject.GetComponent<Text>();
            text.text = Strikethrough(text.text);
            if(!TryShowNextQuest())
                TryLoadNextLevel();
            return true;
        }

        public void TryLoadNextLevel()
        {
            StartCoroutine(NextLevelCoroutine());
        }

        private IEnumerator<WaitForSeconds> NextLevelCoroutine()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(nextSceneName);
        }

        public void ShowQuestUI()
        {
            questCanvas.enabled = true;
        }

        public void HideQuestUI()
        {
            questCanvas.enabled = false;
        }
    }
}