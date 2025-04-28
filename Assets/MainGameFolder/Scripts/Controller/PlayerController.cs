using UnityEngine;
using UnityEngine.InputSystem;
using EntityBase;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private CharacterMotor playerMotor;
    private float SinWASD = Mathf.Sin(-45);
    private float CosWASD = Mathf.Cos(-45);

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
        playerMotor.SetCharacterMove(playerActions.MoveGS.ReadValue<Vector2>());
        playerMotor.SetCharacterMove(
            ConvertWASDContrllerDirection(
                playerActions.MoveWASD.ReadValue<Vector2>()));
    }

    private Vector2 ConvertWASDContrllerDirection(Vector2 direction)
    {
        float x = direction.x * CosWASD - direction.y * SinWASD;
        float y = direction.x * SinWASD + direction.y * CosWASD;
        return new Vector2(x, y);
    }
}