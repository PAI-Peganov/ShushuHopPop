using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndingManager : MonoBehaviour
{
    [FormerlySerializedAs("History")]
    [SerializeField]
    private Canvas History;

    private int historyLabel = -1;

    // Starts the game from saved state
    public void Play()
    {
        historyLabel = 0;
        Time.timeScale = 0; 
    }

    void Update()
    {
        History.enabled = (historyLabel != -1);
        var comp = History.GetComponentsInChildren<TextMeshProUGUI>();
        for (var i = 0; i < comp.Length; i++)
            comp[i].enabled = i == historyLabel;

        if (historyLabel >= 0 && Input.GetKeyDown(KeyCode.Mouse0))
            historyLabel += 1;

        if (historyLabel == 6)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
