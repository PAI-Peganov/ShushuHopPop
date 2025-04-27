using UnityEngine;
using UnityEngine.InputSystem;
using EntityBase;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private CharacterMotor playerMotor;
    private float stickSin = Mathf.Sin(-45);
    private float stickCos = Mathf.Cos(-45);

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
        playerMotor.SetCharacterMove(playerActions.MoveWASD.ReadValue<Vector2>());
        playerMotor.SetCharacterMove(
            ConvertStickContrllerDirection(
                playerActions.MoveGS.ReadValue<Vector2>()));
    }

    private Vector2 ConvertStickContrllerDirection(Vector2 direction)
    {
        float x = direction.x * stickCos - direction.y * stickSin;
        float y = direction.x * stickSin + direction.y * stickCos;
        return new Vector2(x, y);
    }
}