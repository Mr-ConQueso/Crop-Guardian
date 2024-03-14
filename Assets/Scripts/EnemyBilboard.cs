using System;
using UnityEngine;

public class EnemyBilboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(90, 0, 0);
    }
}
