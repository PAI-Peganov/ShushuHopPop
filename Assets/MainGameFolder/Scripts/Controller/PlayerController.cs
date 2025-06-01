using UnityEngine;
using UnityEngine.InputSystem;
using EntityBase;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isCellDependent;
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private ICharacterMotor playerMotor;

    public bool AttackButtonClicked => playerActions.Attack.IsPressed();

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        if (isCellDependent)
            playerMotor = GetComponent<CharacterMotorCellDependent>();
        else
            playerMotor = GetComponent<CharacterMotorIndependent>();
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
        playerMotor.SetCharacterMove(
            playerActions.MoveGS.ReadValue<Vector2>(),
            playerActions.MoveWASD.ReadValue<Vector2>());
    }
}