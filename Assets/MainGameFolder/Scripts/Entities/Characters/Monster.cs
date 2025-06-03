using EntityBase;
using MainGameFolder.Scripts.UI.QuestsWindow;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    [SerializeField] private float attackCalldown;
    [SerializeField] private float attackedCalldown;

    public AnimationsSoundsCaster ASCaster { get; private set; }
    private CharacterMotorIndependent motor;
    private float canAttackMoment;

    public bool IsAttacking { get; private set; } = false;
    public bool IsSeeingPlayer { get; set; } = false;

    new void Awake()
    {
        base.Awake();
        DashDistance = dashDistance;
        ASCaster = GetComponent<AnimationsSoundsCaster>();
        motor = GetComponent<CharacterMotorIndependent>();
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
            SetIsWaiting();
            return true;
        }
        return false;
    }

    public new bool TrySetIsDashing()
    {
        DashDistance = Mathf.Clamp(
            Vector3.Distance(WorldManager.PlayerPosition, transform.position) - 0.3f,
            0f,
            dashDistance);
        return base.TrySetIsDashing();
    }

    public new float AttackDamage
    {
        get
        {
            if (CanAttack)
            {
                canAttackMoment = Time.time + attackCalldown;
                IsAttacking = false;
                SetIsWaiting();
                ASCaster.SetSpriteAttack();
                StartCoroutine(SetCoroutine(SetIsNotWaiting, 1f));
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
        SetIsWaiting();
        if (healthPoints <= 0)
        {
            if (TryGetComponent<Item>(out var taskItem))
                foreach (var questName in taskItem.QuestsNames)
                    WorldManager.CompleteQuest(questName);
            motor.enabled = false;
            ASCaster.PlaySoundByName("MonsterDeath");
            ASCaster.PlayAnimationByName("MonsterDeath");
            StartCoroutine(SetCoroutine(() => gameObject.SetActive(false), 1f));
            return;
        }
        StartCoroutine(SetCoroutine(SetIsNotWaiting, attackedCalldown));
    }
}
