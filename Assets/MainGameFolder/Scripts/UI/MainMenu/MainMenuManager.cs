using System;
using Unity.VisualScripting;
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

        // Starts the game from saved state
        public void Play()
        {
            //TODO: load game state
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Enables settings scene
        public void Settings()
        {
            menu.enabled = false;
            settingsMenu.enabled = true;
        }

        public void QuitSettings()
        {
            settingsMenu.enabled = false;
            menu.enabled = true;
        }

        // Exists from game
        public void Exit()
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }
}