using EntityBase;
using System;
using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    private GameObject healthBar;
    private float lengthMultiplier = 1f;
    private Func<bool> changeBarVisibility;

    private Entity entity;
    private void Update()
    {
        healthBar.SetActive(changeBarVisibility());

        var curHealth = healthBar.transform.GetChild(1);
        var curHealthScale = curHealth.transform.localScale;
        curHealthScale.x = entity.GetHealthPercent() * lengthMultiplier;
        curHealth.transform.localScale = curHealthScale;
    }

    private void Start()
    {
        var player = GetComponent<Player>();
        if (player != null)
        {
            entity = player;
            healthBar = Instantiate(healthBarPrefab, new Vector3(0, 0.8f, 0), Quaternion.identity);
            healthBar.transform.SetParent(transform, false);
            healthBar.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 144);
            changeBarVisibility = () => true;
            return;
        }

        var monster = GetComponent<Monster>();
        if (monster != null)
        {
            entity = monster;
            healthBar = Instantiate(healthBarPrefab, new Vector3(0, 0.8f, 0), Quaternion.identity);
            healthBar.transform.SetParent(transform, false);
            healthBar.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 144);
            changeBarVisibility = () => monster.IsSeeingPlayer;
            return;
        }
    }

    
}