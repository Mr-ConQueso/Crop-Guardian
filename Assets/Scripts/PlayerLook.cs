using UnityEditor;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("Horizontal")]
    [SerializeField] private float horizontalRotationSpeed = 5.0f;
    [Header("Vertical")]
    [SerializeField] private float verticalRotationSpeed = 5.0f;
    [SerializeField] private bool invertVerticalAxis = true;
    [SerializeField] private float maxHeightAngle = 80;
    [SerializeField] private float minHeightAngle = 20;
    
    // ---- / Private Variables / ---- //
    private float _currentAngle;

    private void Start()
    {
        _currentAngle = (maxHeightAngle + minHeightAngle) / 2;
    }

    void Update()
    {
        float verticalRotation = Input.GetAxis("Mouse Y") * verticalRotationSpeed * GetControlsInverted();
        float horizontalRotation = Input.GetAxis("Horizontal") * horizontalRotationSpeed;

        horizontalRotation *= Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0, Space.World);


        float playerPitch = ClampAngle(transform.localEulerAngles.x, 30, 300);
        Debug.Log(playerPitch);
        
        
        transform.Rotate(verticalRotation, 0, 0, Space.Self);
    }
    /// <summary>
    /// Returns 1 or -1 based on the controller inversion
    /// </summary>
    /// <returns></returns>
    private int GetControlsInverted()
    {
        if (invertVerticalAxis)
        {
            return -1;
        }
        return 1;
    }
    
    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360+from);
        return Mathf.Min(angle, to);
    }
}
