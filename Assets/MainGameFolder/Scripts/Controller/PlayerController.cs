using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isCellDependent;
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private PlayerFightSystem fightSystem;
    private ICharacterMotor playerMotor;

    public bool InteractButtonClicked => playerActions.Interact.IsPressed();

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        if (isCellDependent)
            playerMotor = GetComponent<CharacterMotorCellDependent>();
        else
            playerMotor = GetComponent<CharacterMotorIndependent>();
        fightSystem = GetComponent<PlayerFightSystem>();
        playerActions.Attack.started += ctx => fightSystem.PerformNextQTE();
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