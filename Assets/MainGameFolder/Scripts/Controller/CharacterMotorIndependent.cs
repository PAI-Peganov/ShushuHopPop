using EntityBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMotorIndependent : MonoBehaviour, ICharacterMotor
{
    private CharacterController characterController;
    private Entity character;
    private AnimationsSwitcher animationsSwitcher;
    private float lastStepMoment;
    private float entityMoveTimeout => character.MoveTimeout;
    private float entityMoveSpeed => character.MoveSpeed;

    private Vector3 startMovePosition;
    private Vector3 aimMovePosition;
    private Vector3 moveDirection;

    public float CurrentTime => Time.realtimeSinceStartup;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lastStepMoment = 0f;
        character = GetComponent<Entity>();
        animationsSwitcher = GetComponent<AnimationsSwitcher>();
    }

    void Start()
    {
        aimMovePosition = WorldManager.PlayerStart;
        characterController.Move(aimMovePosition);
        moveDirection = new Vector3(1.7f, 1f, 0f);
        aimMovePosition += moveDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TryMoveCharacter();
    }

    public void SetCharacterMove(Vector2 controllerDirection)
    {
        if (controllerDirection.magnitude > 0.1f)
        {
            var calculatedDirection = CalculateMove(controllerDirection);
            if (Vector3.Dot(calculatedDirection, moveDirection) > 0.95f)
                lastStepMoment = CurrentTime;
            if (CurrentTime - lastStepMoment > 0.05f)
            {
                moveDirection = calculatedDirection;
                animationsSwitcher.SetSpriteWalkingByEightDirections(
                    new Vector2(moveDirection.x, moveDirection.y));
            }
            startMovePosition = characterController.transform.position;
            aimMovePosition = startMovePosition + moveDirection;
            character.IsMoving = true;
        }
        else if (character.IsMoving)
        {
            character.IsMoving = false;
            StartCoroutine(CorotineEnumerator(0.1f, () => {
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
            characterController.Move(moveDirection * entityMoveSpeed * Time.deltaTime);
    }

    private Vector3 CalculateMove(Vector2 controllerDirection) =>
        new Vector3(controllerDirection.x * 1.7f, controllerDirection.y, 0).normalized;
}
