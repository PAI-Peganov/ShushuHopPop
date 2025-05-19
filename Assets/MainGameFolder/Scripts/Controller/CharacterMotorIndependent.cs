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
    private AnimationsSwitcher animationsSwitcher;
    private float lastStepMoment;
    private float entityMoveTimeout => character.MoveTimeout;
    private float entityMoveSpeed => character.MoveSpeed;

    private Vector3 startMovePosition;
    private Vector3 aimMovePosition;
    private Vector3 moveDirection;
    public Vector3 MoveDirection => new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

    public float CurrentTime => Time.realtimeSinceStartup;

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
        character = GetComponent<Entity>();
        animationsSwitcher = GetComponent<AnimationsSwitcher>();
    }

    void Start()
    {
        aimMovePosition = WorldManager.PlayerStart;
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
        var controllerDirection = controllerDirections
            .FirstOrDefault(dir => dir.magnitude > 0.1f);
        if (controllerDirection.magnitude > 0.1f)
        {
            var calculatedDirection = CalculateMove(controllerDirection);
            if (Vector3.Dot(calculatedDirection.normalized, moveDirection.normalized) > 0.9f)
                lastStepMoment = CurrentTime;
            if (CurrentTime - lastStepMoment > 0.05f)
            {
                moveDirection = calculatedDirection;
                animationsSwitcher.SetSpriteWalkingByEightDirections(
                    new Vector2(moveDirection.x, moveDirection.y));
            }
            startMovePosition = transform.position;
            aimMovePosition = startMovePosition + moveDirection;
            if (!character.IsMoving)
            {
                character.IsMoving = true;
                animationsSwitcher.SetSpriteWalkingByEightDirections(
                    new Vector2(moveDirection.x, moveDirection.y));
            }
        }
        else if (character.IsMoving)
        {
            character.IsMoving = false;
            StartCoroutine(CorotineEnumerator(Time.deltaTime * 5, () =>
            {
                if (!character.IsMoving)
                    animationsSwitcher.SetSpriteStanding();
            }));
        }
    }

    private IEnumerator CorotineEnumerator(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    private void TryMoveCharacter()
    {
        if (character.IsMoving)
        {
            transform.position += moveDirection * entityMoveSpeed * Time.deltaTime;
            WorldManager.UpdatePlayerLocation(transform.position);
        }
    }

    private Vector3 CalculateMove(Vector2 controllerDirection) =>
        eightDirections.OrderByDescending(dir => (dir + controllerDirection).magnitude)
        .Select(vec => new Vector3(vec.x * 1.7f, vec.y, 0))
        .First();
}
