using EntityBase;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerFightSystem fightSystem;
    private Player player;
    private PlayerController playerController;
    private AnimationsSoundsCaster ASCaster;

    void Awake()
    {
        fightSystem = GetComponent<PlayerFightSystem>();
        player = GetComponent<Player>();
        playerController = GetComponent<PlayerController>();
        ASCaster = GetComponent<AnimationsSoundsCaster>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Mushroom":
                foreach (Light2D lighter in FindObjectsByType<Light2D>(FindObjectsSortMode.None))
                {
                    lighter.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
                    lighter.intensity = 0.01f;
                    if (lighter.lightType == Light2D.LightType.Global)
                        lighter.intensity = 0.0001f;
                }

                break;
            default:
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                var enemy = collision.gameObject.GetComponent<Monster>();
                if (enemy.TryStartAttack())
                    fightSystem.StartFightMonster(enemy);
                break;
            case "Interactable":
                if (playerController.InteractButtonClicked)
                {
                    ASCaster.PlaySoundByName(collision.gameObject.GetComponent<Item>().InteractionSoundName);
                    foreach (var questName in collision.gameObject.GetComponent<Item>().QuestsNames)
                        WorldManager.CompleteQuest(questName);
                }

                break;
            default:
                break;
        }
    }
}