using System;
using System.Collections;
using UnityEngine;

namespace EntityBase
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected int maxHealthPoints;
        [SerializeField] protected int healthPoints;

        [SerializeField] protected float defaultMoveTimeout;
        [SerializeField] protected float defaultMoveSpeed;
        [SerializeField] protected float dashPrepaireTime;
        [SerializeField] protected float dashSpeed;
        [SerializeField] protected float dashDistance;
        [SerializeField] protected float dashCalldownTime;

        [SerializeField] protected float defaultDamage;
        [SerializeField] protected float defaultResistanse;

        [SerializeField] protected string id;
        [SerializeField] protected Vector2Int startCell;

        public string Id => id;
        public Vector2Int StartCell => startCell;
        public Vector3 StartPosition => WorldManager.GetWorldPositionFromCell(StartCell);
        public float MoveTimeout { private set; get; }
        public float MoveSpeed { protected set; get; }
        public float DashDistance => dashDistance;
        public float DashPrepaireTime => dashPrepaireTime;
        public float DashSpeed => dashSpeed;
        public float DashCalldownTime => dashCalldownTime;
        public bool IsMoving { private set; get; }
        public bool IsDashing { private set; get; }
        public bool IsWaiting { private set; get; }
        public float AttackDamage => defaultDamage;
        public float Resistance => defaultResistanse;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected void Awake()
        {
            MoveTimeout = defaultMoveTimeout;
            MoveSpeed = defaultMoveSpeed;
            IsMoving = false;
            IsDashing = false;
            IsWaiting = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(float damage)
        {
            healthPoints -= (int)damage;
        }

        public bool TrySetIsMoving()
        {
            IsMoving = !(IsWaiting || IsDashing);
            return IsMoving;
        }

        public void SetNotIsMoving()
        {
            IsMoving = false;
        }

        public bool TrySetIsDashing()
        {
            if (IsDashing || IsWaiting)
                return false;
            IsMoving = false;
            IsWaiting = true;
            StartCoroutine(SetCoroutine(() =>
            {
                IsWaiting = false;
                IsDashing = true;
                MoveSpeed = dashSpeed;
                StartCoroutine(SetCoroutine(() =>
                {
                    IsDashing = false;
                    MoveSpeed = defaultMoveSpeed;
                }, DashDistance / dashSpeed));
            },
            dashPrepaireTime));
            return true;
        }

        public void SetIsWaiting()
        {
            IsDashing = false;
            IsMoving = false;
            IsWaiting = true;
            MoveSpeed = defaultMoveSpeed;
        }

        public void SetIsNotWaiting()
        {
            IsWaiting = false;
        }

        protected IEnumerator SetCoroutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}
