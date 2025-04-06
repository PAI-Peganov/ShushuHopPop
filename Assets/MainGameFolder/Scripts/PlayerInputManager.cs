using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private PlayerMotor playerMotor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        playerMotor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.PlayerMove(playerActions.Move.ReadValue<Vector2>());
        Debug.Log(playerActions.Move.ReadValue<Vector2>());
    }
}
