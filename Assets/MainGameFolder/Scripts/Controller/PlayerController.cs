using UnityEngine;
using UnityEngine.InputSystem;
using EntityBase;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private CharacterMotor playerMotor;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        playerMotor = GetComponent<CharacterMotor>();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    void FixedUpdate()
    {
        playerMotor.PlayerMove(playerActions.Move.ReadValue<Vector2>());
    }
}