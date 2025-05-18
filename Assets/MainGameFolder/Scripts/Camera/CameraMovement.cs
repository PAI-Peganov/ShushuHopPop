using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float movingOffset;
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    private Camera playerCamera;
    private Vector3 targetPosition;
    private float cameraToTargetDistance;
    private bool isGoing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerCamera.transform.SetParent(null);
        targetPosition = playerCamera.transform.position;
        isGoing = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateGoing();
        TryUpdateCameraPosition();
    }

    private void TryUpdateCameraPosition()
    {
        if (isGoing)
            playerCamera.transform.position =
                Vector3.Lerp(playerCamera.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }

    private void UpdateGoing()
    {
        targetPosition.Set(transform.position.x,
                           transform.position.y,
                           targetPosition.z);
        cameraToTargetDistance = Vector3.Distance(targetPosition, playerCamera.transform.position);
        if (cameraToTargetDistance > movingOffset)
            isGoing = true;
        else if (cameraToTargetDistance < Vector3.kEpsilon)
            isGoing = false;
    }
}
