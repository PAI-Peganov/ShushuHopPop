using EntityBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerFightSystem : MonoBehaviour
{
    [SerializeField] private GameObject fightUI;
    [SerializeField] private GameObject circleAtack;
    [SerializeField] private float initTime;
    [SerializeField] private float slashTime;
    //[SerializeField] private KeyCode[] parryKeys;
    //private Queue<KeyCode> keys;
    private TextMeshPro textNextKey;
    private Player player;
    private PlayerController controller;
    private AnimationsSoundsCaster caster;
    private int isQTERunning = 0;
    private int currentQTENum = 0;
    private int countQTE = 0;

    private float currentTime => Time.time;
    
    void Awake()
    {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();
        caster = GetComponent<AnimationsSoundsCaster>();
        textNextKey = fightUI.GetComponentInChildren<TextMeshPro>();
        //keys = new Queue<KeyCode>();
    }

    void Start()
    {
        fightUI.SetActive(false);
    }

    void FixedUpdate()
    {
        //if (keys.TryPeek(out var key))
        //    textNextKey.SetText(key.ToString());
    }

    public void StartFightMonster(Monster monster)
    {
        StartCoroutine(QTECoroutine(monster, countQTE++));
    }

    private IEnumerator QTECoroutine(Monster monster, int number)
    {
        isQTERunning++;
        player.SetIsWaiting();
        fightUI.SetActive(true);

        //keys.Enqueue(parryKeys[Random.Range(0, parryKeys.Length)]);

        var current = Instantiate(circleAtack, fightUI.transform);
        var spriteRenderer = current.GetComponent<SpriteRenderer>();
        current.transform.localScale = Vector3.one * 40;
        spriteRenderer.color = new Color(0.1f, 0.1f, 0.1f, 1f);

        yield return null;
        var endBound = currentTime + initTime;
        while (currentTime < endBound)
        {
            current.transform.localScale -= Vector3.one * 10 * (Time.deltaTime / initTime);
            yield return null;
        }

        current.transform.localScale = Vector3.one * 30;
        spriteRenderer.color = new Color(0f, 1f, 1f, 1f);

        yield return null;
        var saveTimeBound = currentTime + slashTime * 30 / 41;
        endBound = currentTime + slashTime;
        while (currentTime < endBound)
        {
            if (currentTime > saveTimeBound)
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
            current.transform.localScale -= Vector3.one * 30 * (Time.deltaTime / slashTime);
            if (currentQTENum == number && controller.AttackButtonClicked)
            {
                if (currentTime < saveTimeBound)
                {
                    caster.PlaySoundByName("BlockAtack");
                    player.TakeDamage(monster.AttackDamage * (1 - player.Resistance));
                }
                else
                {
                    caster.PlaySoundByName("Parry");
                    monster.TakeDamage(player.AttackDamage);
                }
                break;
            }
            yield return null;
        }
        currentQTENum++;
        if (currentTime > endBound)
        {
            caster.PlaySoundByName("TakeDamage");
            player.TakeDamage(monster.AttackDamage);
        }
        Destroy(current);
        isQTERunning--;
        if (isQTERunning == 0)
            fightUI.SetActive(false);
        player.SetIsNotWaiting();
        //keys.Dequeue();
    }
}
