using EntityBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMotorIndependent : MonoBehaviour, ICharacterMotor
{
    private Entity character;
    private AnimationsSoundsCaster animationsSwitcher;
    private float lastStepMoment;
    private float lastDashMoment;
    private float entityMoveTimeout => character.MoveTimeout;
    private float entityMoveSpeed => character.MoveSpeed;

    private Vector3 startMovePosition;
    private Vector3 aimMovePosition;
    private Vector3 moveDirection;

    public Vector3 MoveDirection => new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
    public float CurrentTime => Time.time;

    private readonly Vector2[] eightDirections = new Vector2[]
    {
        new Vector2(0.71f, 0.71f).normalized,
        new Vector2(0.71f, -0.71f).normalized,
        new Vector2(-0.71f, -0.71f).normalized,
        new Vector2(-0.71f, 0.71f).normalized,
        new (1, 0),
        new (0, -1),
        new (-1, 0),
        new (0, 1)
    };

    void Awake()
    {
        lastStepMoment = 0f;
        lastDashMoment = 0f;
        character = GetComponent<Entity>();
        animationsSwitcher = GetComponent<AnimationsSoundsCaster>();
    }

    void Start()
    {
        aimMovePosition = character.StartPosition;
        transform.position = aimMovePosition;
        moveDirection = new Vector3(1.7f, 1f, 0f);
        aimMovePosition += moveDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TryMoveCharacter();
    }

    public void SetCharacterMove(params Vector2[] controllerDirections)
    {
        if (character.IsDashing)
            return;
        var controllerDirection = controllerDirections
            .FirstOrDefault(dir => dir.magnitude > 0.1f);
        if (controllerDirection.magnitude > 0.1f)
        {
            if (CurrentTime > lastStepMoment + 0.15f)
            {
                moveDirection = CalculateMove(controllerDirection);
                lastStepMoment = CurrentTime;
            }
            startMovePosition = transform.position;
            aimMovePosition = startMovePosition + moveDirection;
            if (character.TrySetIsMoving())
                animationsSwitcher.SetSpriteWalkingByEightDirections(
                    new Vector2(moveDirection.x, moveDirection.y));
            else
                animationsSwitcher.SetSpriteStanding();
        }
        else if (character.IsMoving)
        {
            character.SetNotIsMoving();
            StartCoroutine(CorotineEnumerator(0.05f,
                () =>
                {
                    if (!character.IsMoving)
                        animationsSwitcher.SetSpriteStanding();
                }));
        }
    }

    public bool TryCallCharacterDash()
    {
        if (character.IsDashing)
            lastDashMoment = CurrentTime;
        if (lastDashMoment + character.DashCalldownTime < CurrentTime && character.TrySetIsDashing())
        {
            animationsSwitcher.SetSpriteStanding();
            StartCoroutine(CorotineEnumerator(character.DashPrepaireTime,
                () =>
                {
                    animationsSwitcher.SetSpriteWalkingByEightDirections(moveDirection, true);
                    animationsSwitcher.SetAnimationSpeed(0.5f);
                }));
            return true;
        }
        return false;
    }

    private IEnumerator CorotineEnumerator(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    private void TryMoveCharacter()
    {
        if (character.IsMoving || character.IsDashing)
        {
            transform.position += moveDirection * entityMoveSpeed * Time.deltaTime;
        }
    }

    private Vector3 CalculateMoveEightDirections(Vector2 controllerDirection) =>
        eightDirections.OrderByDescending(dir => (dir + controllerDirection).magnitude)
        .Select(vec => new Vector3(vec.x * 1.7f, vec.y, 0))
        .First();

    private Vector3 CalculateMove(Vector2 controllerDirection)
    {
        var vec = controllerDirection.normalized;
        return new Vector3(vec.x * 1.7f, vec.y, 0);
    }
}
