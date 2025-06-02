using Mono.Cecil;
using UnityEngine;
using UnityEngine.Rendering;
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

        private int history_label = 0;

        // Starts the game from saved state
        public void Play()
        { 
            history_label = 1;
        }

        void Update()
        {

            History.enabled = (history_label == 1);
            

            if (history_label > 0 && Input.GetKeyDown(KeyCode.Mouse0))
            {
                history_label += 1;
            }

            if (history_label == 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
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