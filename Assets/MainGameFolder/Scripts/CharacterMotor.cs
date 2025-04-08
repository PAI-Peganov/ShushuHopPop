using ShushuHopPop;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    private readonly Vector3[] directionVector = new Vector3[]
    {
        Vector3.left, Vector3.right, Vector3.up, Vector3.down
    };

    private GameManager manager;
    private CharacterController characterController;
    private Character character;
    private float lastStepMoment;
    private float playerMoveTimeout => character.MoveTimeout;

    public float CurrentTime => Time.realtimeSinceStartup;
    public float CanMoveTimer => 1 - (CurrentTime - lastStepMoment) / playerMoveTimeout;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lastStepMoment = Time.realtimeSinceStartup;
        character = GetComponent<Character>();
        manager = gameObject.AddComponent<GameManager>();
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

    private Vector3 MakeVector3(Direction direction) =>
        directionVector[(int)direction];

}
