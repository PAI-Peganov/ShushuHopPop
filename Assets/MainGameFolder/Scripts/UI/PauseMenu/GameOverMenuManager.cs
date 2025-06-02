using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGameFolder.Scripts.UI.PauseMenu
{
    public class GameOverMenuManager : MonoBehaviour
    {
        public static GameOverMenuManager Instance { get; private set; }

        [SerializeField] private Canvas gameOverMenu;
        

        void Start()
        {
            gameOverMenu.enabled = false;
            Instance = this;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OverGame();
            }
        }

        public void OverGame()
        {
            Time.timeScale = 0f;
            gameOverMenu.enabled = true;
        }
        public void Resume()
        {
            gameOverMenu.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}