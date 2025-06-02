using MainGameFolder.Scripts.UI.Quest;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityBase
{
    public class Player : Entity
    {
        private PlayerController inputManager;
        private AnimationsSoundsCaster ASCaster;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        new void Awake()
        {
            base.Awake();
            inputManager = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            WorldManager.UpdatePlayerLocation(transform.position);
        }

        public new void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (healthPoints <= 0)
            {
                ASCaster.PlaySoundByName("PlayerDeath");
                ASCaster.PlayAnimationByName("PlayerDeath");
                gameObject.SetActive(false);
            }
        }
    }
}
