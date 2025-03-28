using UnityEngine;

namespace ShushuHopPop
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private Rigidbody rb;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
