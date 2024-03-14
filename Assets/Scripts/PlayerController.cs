using System;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("Shooting")]
    [SerializeField] private float nextAllowedShotTime = 0.01f;
    [SerializeField] private float bulletRecoveryTime = 2.0f;
    [SerializeField] private float shootDistance = 10.0f;
    
    [Header("Bulltes")]
    [SerializeField] private TMP_Text bulletsLeftAmount;
    [SerializeField] private int initialBulletAmount = 30;
    
    [Header("Aiming")]
    [SerializeField] private  float aimTransitionDuration = 0.2f;
    [SerializeField] private float defaultFOV = 60.0f;
    [SerializeField] private float zoomedFOV = 30.0f;
    
    // ---- / Private Variables / ---- //
    private float _timeSinceLastShot = 0.0f;
    private MeshRenderer _colliderRenderer;
    private Camera _mainCamera;
    private int _bulletAmount;
    private bool isAiming = false;
    private float aimTransitionStartTime;
    
    private Vector2 _screenPoint = new Vector2(960.0f, 378.0f);

    private void Start()
    {
        _mainCamera = Camera.main;
        _colliderRenderer = GetComponentInChildren<MeshRenderer>();
        _colliderRenderer.enabled = false;

        _bulletAmount = initialBulletAmount;
        InvokeRepeating(nameof(AddBullet), bulletRecoveryTime, bulletRecoveryTime);
        
        SetBulletAmountText(0);
    }

    private void Update()
    {
        Aim();
        
        _timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && _timeSinceLastShot >= nextAllowedShotTime)
        {
            Shoot();    
            _timeSinceLastShot = 0.0f; // Reset the timer
        }
    }

    private void Aim()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isAiming = true;
            aimTransitionStartTime = Time.time;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isAiming = false;
            aimTransitionStartTime = Time.time;
        }

        if (isAiming)
        {
            float t = (Time.time - aimTransitionStartTime) / aimTransitionDuration;
            _mainCamera.fieldOfView = Mathf.Lerp(defaultFOV, zoomedFOV, t);
        }
        else
        {
            float t = (Time.time - aimTransitionStartTime) / aimTransitionDuration;
            _mainCamera.fieldOfView = Mathf.Lerp(zoomedFOV, defaultFOV, t);
        }
    }
    private void Shoot()
    {
        if (_bulletAmount >= 1)
        {
            // Cast a ray from the specific point on the screen
            Ray ray = _mainCamera.ViewportPointToRay(_screenPoint);

            // Draw the bullet trajectory using Debug.DrawLine
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red, 0.1f);

            // Check if the ray hits any objects
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, shootDistance))
            {
                // Check if the hit object has the "Enemy" tag
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Destroy the hit object
                    Destroy(hit.collider.gameObject);
                }
            }
            
            SetBulletAmountText(1);
        }
    }

    private void SetBulletAmountText(int reduceBulletAmount)
    {
        _bulletAmount -= reduceBulletAmount;
        bulletsLeftAmount.text = _bulletAmount.ToString();
    }

    private void AddBullet()
    {
        _bulletAmount++;
        SetBulletAmountText(0);
    }
    
    /*
    void Shoot()
    {
        // Define a point in the center of the screen
        Vector3 centerScreenPoint = new Vector3(0.5f, 0.5f, 0f);

        // Cast a ray from the camera through the center of the screen
        Ray ray = _mainCamera.ViewportPointToRay(centerScreenPoint);

        // Draw the bullet trajectory using Debug.DrawLine
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red, 0.1f);

        // Check if the ray hits any objects
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, shootDistance))
        {
            // Check if the hit object has the "Enemy" tag
            if (hit.collider.CompareTag("Enemy"))
            {
                // Destroy the hit object
                Destroy(hit.collider.gameObject);
            }
        }
    }
    */
}
