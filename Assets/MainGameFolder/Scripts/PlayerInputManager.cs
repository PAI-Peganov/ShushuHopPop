using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private PlayerInput.UIActions uiActions;
    private PlayerMotor playerMotor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        uiActions = playerInput.UI;
        playerMotor = GetComponent<PlayerMotor>();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.PlayerMove(playerActions.Move.ReadValue<Vector2>());
    }
}
