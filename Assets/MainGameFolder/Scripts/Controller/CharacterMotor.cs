using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using EntityBase;

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

    public void PlayerMove(Vector2 direction)
    {
        if (direction != Vector2.zero && CanMoveTimer <= 0f)
        {
            characterController.Move(transform.TransformDirection(new Vector3(direction.x, direction.y)));
            lastStepMoment = CurrentTime;
        }
    }
}
