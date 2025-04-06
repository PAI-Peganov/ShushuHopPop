using UnityEngine;

namespace ShushuHopPop
{
    public class Player : Character
    {
        private PlayerInputManager inputManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            inputManager = GetComponent<PlayerInputManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

