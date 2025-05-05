using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGameFolder.Scripts.UI.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        // Starts the game from saved state
        public void Play()  
        {
            //TODO: load game state
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Enables settings scene
        public void Settings()
        {
        }

        // Exists from game
        public void Exit()
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }
}