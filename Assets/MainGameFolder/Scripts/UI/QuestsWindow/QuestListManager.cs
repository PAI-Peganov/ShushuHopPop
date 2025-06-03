using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainGameFolder.Scripts.UI.QuestsWindow
{
    public class QuestListManager : MonoBehaviour
    {
        private Canvas _questsCanvas;

        private Image _questsBackground;

        private readonly Dictionary<int, (QuestTask task, GameObject gameObject)> _quests = new();

        private bool _isShown = true;

        private void Awake()
        {
            _questsCanvas = GetComponent<Canvas>();
            WorldManager.AddQuestListManager(this);
            _questsBackground = GetComponentInChildren<Image>();
            SetupQuests();
            ShowQuestUI();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_isShown)
                    HideQuestUI();
                else
                    ShowQuestUI();
            }
        }

        private static string InsertLineBreaks(string input, int maxLineLength = 40)
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

        private void SetupQuests()
        {
            var quests = GetComponentsInChildren<QuestTask>();
            if (quests == null || quests.Length == 0)
            {
                Debug.LogException(new Exception("Quests not found"));
                return;
            }

            foreach (var quest in quests.OrderBy(quest => quest.Order))
                CreateQuestText(quest, new Vector2(0, -75 * (quest.Order - 1)), quest.Order == 1);
        }

        private void CreateQuestText(QuestTask quest, Vector2 offset, bool isVisible = true)
        {
            var textObj = new GameObject($"Quest number {quest.Order}");
            textObj.transform.SetParent(_questsBackground.transform, false);
            if (!isVisible)
                textObj.SetActive(false);

            var text = textObj.AddComponent<Text>();
            text.text = InsertLineBreaks(quest.QuestText);
            text.fontSize = 24;
            text.color = Color.white * 0.8f;
            text.alignment = TextAnchor.UpperLeft;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.font = Resources.Load<Font>("UI/Fonts/static/Montserrat-Bold");

            var rectTransform = text.GetComponent<RectTransform>();
            // Смещение от левого верхнего угла
            rectTransform.anchoredPosition = new Vector2(-70, 125) + offset;
            // Text size
            rectTransform.sizeDelta = new Vector2(400, 40);

            _quests.Add(quest.Order, (quest, textObj));
        }

        private bool TryShowNextQuest()
        {
            (QuestTask task, GameObject gameObject) nextQuest = (null, null);
            foreach (var quest in _quests)
            {
                if (!quest.Value.task.IsDone)
                {
                    nextQuest = quest.Value;
                    break;
                }

                if (quest.Value.task.IsDone && !quest.Value.gameObject.activeInHierarchy)
                    quest.Value.gameObject.SetActive(true);
            }

            nextQuest.gameObject?.SetActive(true);
            ShowQuestUI();
            
            return nextQuest.task is not null;
        }

        private void ClearQuests()
        {
            foreach (var quest in _quests.Values)
                Destroy(quest.gameObject);
            _quests.Clear();
        }

        public bool TryMarkQuestAsCompleted(string questName, out QuestTask questTask)
        {
            var quest = _quests.FirstOrDefault(q =>
                q.Value.task.Name == questName).Value;
            questTask = quest.task;

            if (quest.task.IsDone)
                return false;
            
            if (quest.task.PreviousQuestTask != null)
            {
                if (!quest.task.PreviousQuestTask.IsDone)
                {
                    Debug.LogException(new Exception("Previous quest is not completed: " + quest.task.PreviousQuestTask));
                    return false;
                }
            }

            quest.task.MakeDone();

            if (!quest.task.IsDone) return false;
            var text = quest.gameObject.GetComponent<Text>();
            text.text = Strikethrough(text.text);
            if (!TryShowNextQuest())
                TryLoadNextLevel();
            return true;
        }
        
        private void TryLoadNextLevel()
        {
            ClearQuests();
            StartCoroutine(NextLevelCoroutine());
        }

        private IEnumerator<WaitForSeconds> NextLevelCoroutine()
        {
            yield return new WaitForSeconds(10);
            if (SceneManager.GetActiveScene().buildIndex < 3)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                GameObject.Find("HistoryManager").GetComponent<EndingManager>().Play();
        }

        private void ShowQuestUI()
        {
            _isShown = true;
            _questsCanvas.enabled = true;
        }

        private void HideQuestUI()
        {
            _isShown = false;
            _questsCanvas.enabled = false;
        }
    }
}