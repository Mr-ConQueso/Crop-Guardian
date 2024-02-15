using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;

    void Update()
    {
        // Get input for rotation
        float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate the camera horizontally
        transform.Rotate(Vector3.up, horizontalRotation, Space.World);

        // Rotate the camera vertically
        transform.Rotate(Vector3.left, verticalRotation, Space.Self);
    }
}
