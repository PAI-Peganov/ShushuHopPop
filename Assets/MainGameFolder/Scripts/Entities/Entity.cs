using UnityEngine;

namespace EntityBase
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private int maxHealthPoints;
        [SerializeField] private int healthPoints; 
        [SerializeField] private float defaultMoveTimeout;
        [SerializeField] private float defaultMoveSpeed;
        [SerializeField] private float defaultDamage;
        [SerializeField] private float defaultResistanse;

        [SerializeField] public string Id { get; }

        public float MoveTimeout { private set; get; }
        public float MoveSpeed { private set; get; }
        public bool IsMoving { set; get; }
        public float GiveDamage => defaultDamage;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected void Awake()
        {
            MoveTimeout = defaultMoveTimeout;
            MoveSpeed = defaultMoveSpeed;
            IsMoving = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(float damage)
        {
            healthPoints -= (int)damage;
        }
    }
}
