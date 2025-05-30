using BaseGame;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private float maxHeightAngle = 120;
    [SerializeField] private float minHeightAngle;
    
    // ---- / Private Variables / ---- //
    private float _currentAngleX;

    private void Start()
    {
        _currentAngleX = (maxHeightAngle + minHeightAngle) / 2;
    }

    private void Update()
    {
        if (!(GameController.Instance.IsGamePaused || GameController.Instance.IsPlayerFrozen))
        {
            float verticalRotation = InputManager.LookVerticalInput * SavedSettings.mouseVerticalSensibility * SavedSettings.invertDirection;
            float horizontalRotation = InputManager.LookHorizontalInput * SavedSettings.mouseHorizontalSensibility;

            SetRotation(horizontalRotation, verticalRotation);
        }
    }
    
    /// <summary>
    /// Calculate and set the horizontal and vertical rotation of the player's camera.
    /// </summary>
    /// <param name="horizontalRotation"></param>
    /// <param name="verticalRotation"></param>
    private void SetRotation(float horizontalRotation, float verticalRotation)
    {
        horizontalRotation *= Time.deltaTime;
        transform.Rotate(0, horizontalRotation, 0, Space.World);
        
        _currentAngleX -= verticalRotation;

        if (_currentAngleX > minHeightAngle && _currentAngleX < maxHeightAngle)
        {
            transform.Rotate(verticalRotation, 0, 0, Space.Self);
        }
        else if (_currentAngleX < minHeightAngle)
        {
            _currentAngleX = minHeightAngle;
        }
        else if (_currentAngleX > maxHeightAngle)
        {
            _currentAngleX = maxHeightAngle;
        }
    }
}
