using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class FlashlightRotater : MonoBehaviour
{
    [SerializeField]
    private Light2D flashLight;
    [SerializeField]
    private float rotationSpeed;
    private CharacterController characterController;
    private Vector3 lastCharacterPosition;
    private Vector3 lightDirection;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lastCharacterPosition = characterController.transform.position;
    }

    void FixedUpdate()
    {
        TryRotate();
    }

    private void TryRotate()
    {
        lightDirection = characterController.transform.position - lastCharacterPosition;
        if (lightDirection.magnitude > 1E-3f)
        {
            flashLight.transform.rotation = Quaternion.RotateTowards(
                flashLight.transform.rotation,
                Quaternion.Euler(0, 0,
                    Vector3.Angle(Vector3.up, lightDirection) * (lightDirection.x < 0 ? 1 : -1)),
                rotationSpeed);
            flashLight.transform.localPosition = Vector3.RotateTowards(
                Vector3.up,
                lightDirection,
                rotationSpeed,
                1).normalized * 0.2f;
            lastCharacterPosition = characterController.transform.position;
        }
    }
}
