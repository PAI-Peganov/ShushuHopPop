using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private Vector2Int homeCell;
    [SerializeField] private float homeDefendingRadius;
    [SerializeField] private float playerDetectingRadius;
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
        homePosition = WorldManager.GetWorldPositionFromCell(homeCell);
        TryFindNewTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterMotor.SetCharacterMove(CalculateDeltaMove());
    }

    private Vector2 CalculateDeltaMove() =>
        new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);

    private void TryFindNewTarget()
    {
        if ((Vector3.Distance(transform.position, WorldManager.PlayerPosition) < playerDetectingRadius &&
             Vector3.Distance(transform.position, homePosition) < homeDefendingRadius) ||
            Vector3.Distance(homePosition, WorldManager.PlayerPosition) < homeDefendingRadius)
        {
            Debug.Log("Player!!!");
            Debug.Log(characterMotor.CurrentTime);
            animator.OpenEyes();
            targetPosition = WorldManager.PlayerPosition;
            if (characterMotor.TryCallCharacterDash())
                animator.FlashEyesDuration(monster.DashPrepaireTime);
            StartCoroutine(SetCoroutine(TryFindNewTarget, Time.deltaTime * 3));
        }
        else
        {
            Debug.Log("PlayerNotFound");
            Debug.Log(characterMotor.CurrentTime);
            animator.CloseEyes();
            targetPosition = CalculateNextTargetPosition();
            StartCoroutine(SetCoroutine(TryFindNewTarget,
                Random.Range(targetSwitchTime - targetSwitchTimeDelta, targetSwitchTime + targetSwitchTimeDelta)));
        }
    }

    private Vector3 CalculateNextTargetPosition()
    {
        if (Vector3.Distance(transform.position, homePosition) > homeDefendingRadius)
            return homePosition;
        var vec = characterMotor.MoveDirection + Random.onUnitSphere * 0.3f;
        vec.z = 0;
        return transform.position + vec.normalized;
    }

    private IEnumerator SetCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
