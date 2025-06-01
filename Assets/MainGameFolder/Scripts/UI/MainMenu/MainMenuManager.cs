using System;
using MainGameFolder.Scripts.UI.Quest;
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

        // [SerializeField]
        // private QuestsManager questsManager;

        // Starts the game from saved state
        public void Play()
        {
            var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            //questsManager.SetupQuestsByScene(nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
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