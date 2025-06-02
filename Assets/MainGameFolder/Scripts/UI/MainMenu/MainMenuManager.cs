using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace MainGameFolder.Scripts.UI.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [FormerlySerializedAs("Menu")] [SerializeField]
        private Canvas menu;

        [FormerlySerializedAs("SettingsMenu")] [SerializeField]
        private Canvas settingsMenu;

        [FormerlySerializedAs("History")]
        [SerializeField]
        private Canvas History;

        private int historyLabel = -1;

        // Starts the game from saved state
        public void Play()
        { 
            historyLabel = 0;            
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //TODO:
        // Enables settings scene
        // public void Settings()
        // {
        //     menu.enabled = false;
        //     settingsMenu.enabled = true;
        // }
        //
        // public void QuitSettings()
        // {
        //     settingsMenu.enabled = false;
        //     menu.enabled = true;
        // }

        // Exists from game
        public void Exit()
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }
}