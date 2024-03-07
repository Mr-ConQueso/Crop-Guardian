using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private float horizontalRotationSpeed = 5.0f;
    [SerializeField] private float verticalRotationSpeed = 5.0f;

    void Update()
    {
        float verticalRotation = Input.GetAxis("Mouse Y") * verticalRotationSpeed * -1;
        float horizontalRotation = Input.GetAxis("Horizontal") * horizontalRotationSpeed;

        horizontalRotation *= Time.deltaTime;
        
        transform.Rotate(0, horizontalRotation, 0, Space.World);

        if (transform.rotation.x > -35.0f && transform.rotation.x < 35.0f)
        {
            transform.Rotate(verticalRotation, 0, 0, Space.Self);   
        }
    }
}
