using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGameFolder.Scripts.UI.PauseMenu
{
    public class PauseManuManager : MonoBehaviour
    {
        [SerializeField] private Canvas pauseMenu;

        private static bool _isPaused;

        void Start()
        {
            pauseMenu.enabled = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPaused)
                    Resume();
                else
                    Pause();
            }
        }

        public void Pause()
        {
            pauseMenu.enabled = true;
            _isPaused = true;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            pauseMenu.enabled = false;
            _isPaused = false;
            Time.timeScale = 1f;
        }

        public void GoToMainMenu()
        {
            Resume();
            SceneManager.LoadScene("MainMenu");
        }
    }
}