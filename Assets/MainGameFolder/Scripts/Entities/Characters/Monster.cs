using EntityBase;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    [SerializeField] private float attackCalldown;
    [SerializeField] private float attackedCalldown;

    [SerializeField] private AnimationsSoundsCaster ASCaster;
    private float canAttackMoment;

    public bool IsAttacking { get; private set; } = false;

    new void Awake()
    {
        base.Awake();
        ASCaster = GetComponent<AnimationsSoundsCaster>();
        canAttackMoment = 0f;
    }

    public bool CanAttack
    {
        get => Time.time > canAttackMoment;
    }

    public bool TryStartAttack()
    {
        if (!IsAttacking && CanAttack)
        {
            IsAttacking = true;
            return true;
        }
        return false;
    }

    public new float AttackDamage
    {
        get
        {
            if (CanAttack)
            {
                canAttackMoment = Time.time + attackCalldown;
                IsAttacking = false;
                return base.AttackDamage;
            }
            return 0f;
        }
    }

    public new void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        IsAttacking = false;
        canAttackMoment = Time.time + attackedCalldown;
        if (healthPoints <= 0)
        {
            ASCaster.PlaySoundByName("MonsterDeath");
            ASCaster.PlayAnimationByName("MonsterDeath");
            gameObject.SetActive(false);
            return;
        }
        SetIsWaiting();
        StartCoroutine(SetCoroutine(SetIsNotWaiting, attackedCalldown));
    }
}
