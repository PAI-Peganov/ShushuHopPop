using UnityEngine;

namespace EntityBase
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField]
        private int maxHealthPoints;
        [SerializeField]
        private int healthPoints; 
        [SerializeField]
        private float defaultMoveTimeout;

        public string id;
        public float MoveTimeout { private set; get; }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected void Awake()
        {
            MoveTimeout = defaultMoveTimeout;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
