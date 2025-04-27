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
    private float playerMoveTimeout => character.MoveTimeout;

    public float CurrentTime => Time.realtimeSinceStartup;
    public float CanMoveTimer => 1 - (CurrentTime - lastStepMoment) / playerMoveTimeout;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lastStepMoment = Time.realtimeSinceStartup;
        character = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCharacterMove(Vector2 fieldDirection)
    {
        if (fieldDirection.magnitude > 0.1f && CanMoveTimer <= 0f)
        {
            characterController.Move(CalculateMove(fieldDirection));
            lastStepMoment = CurrentTime;
        }
    }

    private Vector3 CalculateMove(Vector2 fieldDirection)
    {
        var worldDirection = WorldManager.HalfCellMoveVectors.OrderBy(v => (v + fieldDirection).magnitude).First();
        return new Vector3(worldDirection.x, worldDirection.y, 0);
    }
}
