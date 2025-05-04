using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using EntityBase;
using System.Linq;

public class CharacterMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Entity character;
    private float lastStepMoment;
    private float entityMoveTimeout => character.MoveTimeout;
    private float entityMoveSpeed => character.MoveSpeed;

    private Vector3 startMovePosition;
    private Vector3 aimMovePosition;
    private bool isMovingToGridline;
    private bool isMovingToNextCell;

    public float CurrentTime => Time.realtimeSinceStartup;
    public float CanMoveTimer => character.IsMoving ? 1 : (1 - (CurrentTime - lastStepMoment) / entityMoveTimeout);

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lastStepMoment = Time.realtimeSinceStartup;
        character = GetComponent<Entity>();
    }

    void Start()
    {
        aimMovePosition = WorldManager.PlayerStart;
        characterController.Move(aimMovePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TryMoveCharacter();
    }

    public void SetCharacterMove(Vector2 controllerDirection)
    {
        if (controllerDirection.magnitude > 0.1f && CanMoveTimer <= 0f)
        {
            startMovePosition = characterController.transform.position;
            aimMovePosition = startMovePosition + CalculateMove(controllerDirection);
            character.IsMoving = true;
            isMovingToGridline = true;
        }
    }

    private void TryMoveCharacter()
    {
        if (!character.IsMoving)
            return;
        characterController.transform.position = Vector3.MoveTowards(characterController.transform.position,
                                                                     aimMovePosition,
                                                                     entityMoveSpeed * Time.deltaTime);
        TryChooseNextMove();
    }

    private void TryChooseNextMove()
    {
        if (Vector3.Distance(characterController.transform.position, aimMovePosition) == 0f)
        {
            if (isMovingToGridline)
            {
                isMovingToGridline = false;
                var targetCellCenter = WorldManager.ClarifyPosition(2f * aimMovePosition - startMovePosition);
                isMovingToNextCell = WorldManager.IsCellAvailableForPlayer(targetCellCenter);
                if (isMovingToNextCell)
                    (aimMovePosition, startMovePosition) = (targetCellCenter, aimMovePosition);
                else
                    (aimMovePosition, startMovePosition) = (startMovePosition, aimMovePosition);
                WorldManager.UpdatePlayerLocation(aimMovePosition);
            }
            else
            {
                character.IsMoving = false;
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
