using EntityBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private float CurrentTime => Time.time;

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

    public void PerformNextQTE()
    {
        if (currentQTENum < countQTE)
            currentQTENum++;
    }

    public void StartFightMonster(Monster monster)
    {
        StartCoroutine(QTECoroutine(monster, ++countQTE));
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
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.05f);

        yield return null;
        var endTimeBound = CurrentTime + initTime;
        while (CurrentTime < endTimeBound)
        {
            current.transform.localScale -= Vector3.one * 10 * (Time.deltaTime / initTime);
            yield return null;
        }

        current.transform.localScale = Vector3.one * 30;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        yield return null;
        endTimeBound = CurrentTime + slashTime;
        var firstBound = (Vector3.one * 15.5f).magnitude;
        var secondBound = (Vector3.one * 11f).magnitude;
        while (CurrentTime < endTimeBound)
        {
            current.transform.localScale -= Vector3.one * 30 * (Time.deltaTime / slashTime);
            if (currentQTENum == number)
            {
                var circleValue = current.transform.localScale.magnitude;
                if (firstBound >= circleValue && circleValue >= secondBound)
                {
                    caster.PlaySoundByName("Parry");
                    monster.TakeDamage(player.AttackDamage);
                    endTimeBound++;
                }
                else
                {
                    endTimeBound = 0f;
                }
                break;
            }
            yield return null;
        }
        if (CurrentTime > endTimeBound)
        {
            caster.PlaySoundByName("TakeDamage");
            player.TakeDamage(monster.AttackDamage);
        }
        currentQTENum = number;
        Destroy(current);
        isQTERunning--;
        if (isQTERunning == 0)
        {
            fightUI.SetActive(false);
            player.SetIsNotWaiting();
        }
        //keys.Dequeue();
    }
}
