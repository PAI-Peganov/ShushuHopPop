using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterAI : MonoBehaviour
{
    //[SerializeField] private Vector2Int homeCell;
    [SerializeField] private float playerKeepingRadius;
    [SerializeField] private float playerDetectingRadius;
    [SerializeField] private float homeRadius;
    [SerializeField] private float targetSwitchTime;
    [SerializeField] private float targetSwitchTimeDelta;
    private Monster monster;
    private CharacterMotorIndependent characterMotor;
    private AnimationsSoundsCaster animator;
    private Vector3 homePosition;
    private Vector3 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        monster = GetComponent<Monster>();
        characterMotor = GetComponent<CharacterMotorIndependent>();
        animator = GetComponent<AnimationsSoundsCaster>();
        homePosition = transform.position;
        StartCoroutine(TryFindNewTarget());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterMotor.SetCharacterMove(CalculateDeltaMove());
    }

    private Vector2 CalculateDeltaMove() =>
        new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);

    private IEnumerator TryFindNewTarget()
    {
        yield return new WaitForSeconds(0.5f);
        while (monster.enabled)
        {
            var distanceToPlayer = Distance(transform.position, WorldManager.PlayerPosition);
            if (distanceToPlayer < playerDetectingRadius)
            {
                animator.OpenEyes();
                targetPosition = WorldManager.PlayerPosition;
                homePosition = targetPosition;
                Debug.Log(WorldManager.PlayerPosition);
                if (distanceToPlayer < playerKeepingRadius)
                    if (characterMotor.TryCallCharacterDash())
                        animator.FlashEyesDuration(monster.DashPrepaireTime);
                yield return new WaitForFixedUpdate();
            }
            else
            {
                animator.CloseEyes();
                targetPosition = CalculateNextTargetPosition();
                yield return new WaitForSeconds(Random.Range(
                    targetSwitchTime - targetSwitchTimeDelta,
                    targetSwitchTime + targetSwitchTimeDelta));
            }
        }
    }

    private float Distance(Vector3 a, Vector3 b) =>
        Vector3.Scale(a - b, new Vector3(1f / 1.7f, 1f, 0f)).magnitude;

    private Vector3 CalculateNextTargetPosition()
    {
        if (Distance(homePosition, transform.position) > homeRadius)
            return homePosition;
        var vec = characterMotor.MoveDirection + Random.onUnitSphere * 0.8f;
        vec.z = 0;
        return transform.position + vec.normalized;
    }

    private IEnumerator SetCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
