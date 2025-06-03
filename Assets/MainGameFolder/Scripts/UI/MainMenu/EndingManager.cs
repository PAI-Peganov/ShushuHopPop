using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndingManager : MonoBehaviour
{
    [FormerlySerializedAs("History")]
    [SerializeField]
    private GameObject HistoryGameObject;
    private Canvas History;

    private int historyLabel = 0;

    private void Awake()
    {
        History = HistoryGameObject.GetComponent<Canvas>();
        HistoryGameObject.SetActive(false);
    }

    // Starts the game from saved state
    public void Play()
    {
        StartCoroutine(StartEnd());
    }

    private IEnumerator StartEnd()
    {
        Time.timeScale = 0f;
        HistoryGameObject.SetActive(true);
        while (historyLabel < 6)
        {
            var comp = History.GetComponentsInChildren<TextMeshProUGUI>();
            for (var i = 0; i < comp.Length; i++)
                comp[i].enabled = i == historyLabel;
            yield return null;
            if (historyLabel >= 0 && Input.GetKeyDown(KeyCode.Mouse0))
                historyLabel += 1;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
