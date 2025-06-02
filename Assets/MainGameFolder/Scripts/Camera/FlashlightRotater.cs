using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightRotater : MonoBehaviour
{
    [SerializeField]
    private Light2D flashLight;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 targetDirection;
    private ICharacterMotor characterMotor;

    void Awake()
    {
        characterMotor = GetComponent<CharacterMotorIndependent>();
    }

    void FixedUpdate()
    {
        TryRotate();
    }

    private void TryRotate()
    {
        targetDirection = characterMotor.MoveDirection;
        if (targetDirection.magnitude > 1E-3f)
        {
            flashLight.transform.rotation = Quaternion.RotateTowards(
                flashLight.transform.rotation,
                Quaternion.Euler(0, 0,
                    Vector3.Angle(Vector3.up, targetDirection) * (targetDirection.x < 0 ? 1 : -1)),
                rotationSpeed);
            flashLight.transform.localPosition = Vector3.RotateTowards(
                Vector3.up,
                targetDirection,
                rotationSpeed,
                1).normalized * 0.2f;
        }
    }
}
