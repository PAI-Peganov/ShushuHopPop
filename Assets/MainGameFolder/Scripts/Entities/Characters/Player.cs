using EntityBase;
using MainGameFolder.Scripts.UI.PauseMenu;

public class Player : Entity
{
    private PlayerController inputManager;
    private AnimationsSoundsCaster ASCaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Awake()
    {
        base.Awake();
        inputManager = GetComponent<PlayerController>();
        ASCaster = GetComponent<AnimationsSoundsCaster>();
    }

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
            inputManager.enabled = false;
            StartCoroutine(SetCoroutine(() =>
            {
                gameObject.SetActive(false);
                GameOverMenuManager.Instance.OverGame();
            },
            0.5f));
        }
    }
}
