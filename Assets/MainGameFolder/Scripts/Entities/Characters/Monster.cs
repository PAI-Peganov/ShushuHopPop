using EntityBase;
using UnityEngine;

public class Monster : Entity
{
    [SerializeField] private float atackCalldown;

    private float lastAtackMoment;

    new void Awake()
    {
        base.Awake();
    }

    public bool CanAtack
    {
        get => Time.realtimeSinceStartup > lastAtackMoment + atackCalldown;
    }

    public new float AtackDamage
    {
        get
        {
            if (CanAtack)
            {
                lastAtackMoment = Time.realtimeSinceStartup;
                return defaultDamage;
            }
            return 0f;
        }
    }
}
