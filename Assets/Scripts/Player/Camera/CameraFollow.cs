using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player object
    [SerializeField] private Vector3 offset; // Offset between camera and player
    [SerializeField] private float smoothSpeed = 0.125f; // Speed of smooth camera follow
    private float currentPosX;

    private void LateUpdate()
    {
        // Check if player reference is set
        if (player == null) return;

        // Calculate the desired position of the camera
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}