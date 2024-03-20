using UnityEngine;

public class EnemyBillboard : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(_mainCamera.transform);
        transform.Rotate(90, 0, 0);
    }
}
