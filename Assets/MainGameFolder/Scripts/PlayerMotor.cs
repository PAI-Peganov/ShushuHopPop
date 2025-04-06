using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private GameManager manager;
    private Vector3[] directionVector = new Vector3[]
    {
        Vector3.left, Vector3.right, Vector3.up, Vector3.down
    };
    private CharacterController characterController;
    private float lastStepMoment;
    [SerializeField]
    private float PlayerMoveTimeout = 1.0f;

    public float CurrentTime => Time.realtimeSinceStartup;
    public float CanMoveTimer => 1 - (CurrentTime - lastStepMoment) / PlayerMoveTimeout;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        lastStepMoment = Time.realtimeSinceStartup;
        manager = gameObject.AddComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMove(Vector2 direction)
    {
        if (CanMoveTimer <= 0f)
        {
            characterController.Move(transform.TransformDirection(new Vector3(direction.x, direction.y)));
            lastStepMoment = CurrentTime;
        } 
    }

    private Vector3 MakeVector3(Direction direction) =>
        directionVector[(int)direction];

}
