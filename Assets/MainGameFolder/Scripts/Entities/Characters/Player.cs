using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityBase
{
    public class Player : Entity
    {
        private PlayerController inputManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        new void Awake()
        {
            base.Awake();
            inputManager = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
