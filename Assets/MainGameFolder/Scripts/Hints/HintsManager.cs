using UnityEngine;

namespace MainGameFolder.Scripts.Hints
{
    public class HintsManager : MonoBehaviour
    {
        private Canvas hintUI;

        void Start()    
        {
            hintUI = GetComponentInChildren<Canvas>();
            hintUI.gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("1111");
                hintUI.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                hintUI.gameObject.SetActive(false);
            }
        }
    }
}