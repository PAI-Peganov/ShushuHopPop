using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using EntityBase;
using System.Linq;
using System.Collections;
using System;

public class CharacterMotorCellDependent : MonoBehaviour, ICharacterMotor
{
    private Entity character;
    private AnimationsSoundsCaster animationsSwitcher;
    private float lastStepMoment;
    private float entityMoveTimeout => character.MoveTimeout;
    private float entityMoveSpeed => character.MoveSpeed;

    private Vector3 startMovePosition;
    private Vector3 aimMovePosition;
    private bool isMovingToGridline;
    private bool isMovingToNextCell;

    public Vector3 MoveDirection => aimMovePosition - startMovePosition;
    public float CurrentTime => Time.realtimeSinceStartup;
    public float CanMoveTimer => character.IsMoving ? 1 : (1 - (CurrentTime - lastStepMoment) / entityMoveTimeout);

    void Awake()
    {
        lastStepMoment = 0f;
        character = GetComponent<Entity>();
        animationsSwitcher = GetComponent<AnimationsSoundsCaster>();
    }

    void Start()
    {
        aimMovePosition = character.StartPosition;
        transform.position = aimMovePosition;
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
        if (Math.Abs(controllerDirection.x) > 0.1f &&
            Math.Abs(controllerDirection.y) > 0.1f &&
            CanMoveTimer <= 0f)
        {
            startMovePosition = transform.position;
            var calculatedDirection = CalculateMove(controllerDirection);
            aimMovePosition = startMovePosition + calculatedDirection;
            character.TrySetIsMoving();
            isMovingToGridline = true;
            animationsSwitcher.SetSpriteWalkingByCellDirections(new Vector2(calculatedDirection.x, calculatedDirection.y));
        }
    }

    private IEnumerator CorotineEnumerator(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    private void TryMoveCharacter()
    {
        if (!character.IsMoving)
            return;
        transform.position = Vector3.MoveTowards(transform.position,
                                                                     aimMovePosition,
                                                                     entityMoveSpeed * Time.deltaTime);
        TryChooseNextMove();
    }

    private void TryChooseNextMove()
    {
        if (Vector3.Distance(transform.position, aimMovePosition) == 0f)
        {
            if (isMovingToGridline)
            {
                isMovingToGridline = false;
                var targetCellCenter = WorldManager.ClarifyPosition(2f * aimMovePosition - startMovePosition);
                isMovingToNextCell = WorldManager.IsCellAvailableForEntity(character, targetCellCenter);
                if (isMovingToNextCell)
                    (aimMovePosition, startMovePosition) = (targetCellCenter, aimMovePosition);
                else
                {
                    (aimMovePosition, startMovePosition) = (startMovePosition, aimMovePosition);
                    animationsSwitcher.SetSpriteWalkingByCellDirections(new Vector2(
                        (aimMovePosition - startMovePosition).x,
                        (aimMovePosition - startMovePosition).y));
                }
            }
            else
            {
                character.SetNotIsMoving();
                StartCoroutine(CorotineEnumerator(0.1f, () => {
                    if (!character.IsMoving)
                        animationsSwitcher.SetSpriteStanding();
                }));
                if (isMovingToNextCell)
                    lastStepMoment = CurrentTime;
            }
        }
    }

    private Vector3 CalculateMove(Vector2 controllerDirection)
    {
        var worldDirection = WorldManager.HalfCellMoveVectors
            .OrderBy(v => Vector3.Distance(v, controllerDirection))
            .First();
        return new Vector3(worldDirection.x, worldDirection.y, 0);
    }
}
