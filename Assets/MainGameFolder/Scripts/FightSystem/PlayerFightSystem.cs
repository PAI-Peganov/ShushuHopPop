using EntityBase;
using UnityEngine;

public class PlayerFightSystem : MonoBehaviour
{
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFightMonster(Monster monster)
    {
        player.TakeDamage(monster.GiveDamage);
    }
}
