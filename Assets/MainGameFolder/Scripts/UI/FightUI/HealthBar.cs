using EntityBase;
using System;
using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float healthPercent = 0.1f;

    [SerializeField] private GameObject healthBarPrefab;
    private GameObject healthBar;
    private float lengthMultiplier = 1f;
    private float heightMultiplier = 0.08f;
    private Func<bool> changeBarVisibility;

    private Entity entity;
    private void Update()
    {
        healthPercent = entity.GetHealthPercent();
        if (healthBar != null)
        {
            healthBar.SetActive(changeBarVisibility());

            healthBar.transform.position = transform.position + new Vector3(0, 0.8f, 0);
            var maxHealth = healthBar.transform.GetChild(0);
            var maxHealthScale = maxHealth.transform.localScale;
            maxHealthScale.x = lengthMultiplier + 0.03f;
            maxHealthScale.y = heightMultiplier + 0.03f;
            maxHealth.transform.localScale = maxHealthScale;


            var curHealth = healthBar.transform.GetChild(1);
            var curHealthScale = curHealth.transform.localScale;
            curHealthScale.x = healthPercent * lengthMultiplier;
            curHealthScale.y = heightMultiplier;
            curHealth.transform.localScale = curHealthScale;
        }

        if (healthPercent <= 0)
            Destroy(healthBar);
    }

    private void Start()
    {
        var player = gameObject.GetComponent<Player>();
        if (player != null)
        {
            entity = player;
            healthBar = Instantiate(healthBarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            healthBar.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 144);
            changeBarVisibility = () => true;
            return;
        }

        var monster = gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            entity = monster;
            healthBar = Instantiate(healthBarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            healthBar.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 144);
            changeBarVisibility = () => monster.IsSeeingPlayer;
            return;
        }
    }

    
}