using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("Other")]
    [SerializeField] private float deathRadius;
    
    [Header("Shooting")]
    [SerializeField] private float aimOffset = 10f;
    [SerializeField] private float nextAllowedShotTime = 0.01f;
    [SerializeField] private float bulletRecoveryTime = 2.0f;
    [SerializeField] private float shootDistance = 10.0f;
    
    [Header("Bullets")]
    [SerializeField] private TMP_Text bulletsLeftAmount;
    [SerializeField] private int initialBulletAmount = 30;
    
    [Header("Aiming")]
    [SerializeField] private  float aimTransitionDuration = 0.2f;
    [SerializeField] private float defaultFOV = 60.0f;
    [SerializeField] private float zoomedFOV = 30.0f;
    
    // ---- / Private Variables / ---- //
    private float _timeSinceLastShot;
    private Camera _mainCamera;
    private int _bulletAmount;
    private bool _isAiming;
    private float _aimTransitionStartTime;
    private GameController _gameController;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

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

        DetectDeath();
    }
    
    /// <summary>
    /// Modify the FOV when the user holds RMB
    /// </summary>
    private void Aim()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _isAiming = true;
            _aimTransitionStartTime = Time.time;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            _isAiming = false;
            _aimTransitionStartTime = Time.time;
        }

        if (_isAiming)
        {
            float t = (Time.time - _aimTransitionStartTime) / aimTransitionDuration;
            _mainCamera.fieldOfView = Mathf.Lerp(defaultFOV, zoomedFOV, t);
        }
        else
        {
            float t = (Time.time - _aimTransitionStartTime) / aimTransitionDuration;
            _mainCamera.fieldOfView = Mathf.Lerp(zoomedFOV, defaultFOV, t);
        }
    }
    
    /// <summary>
    /// Shoots a raycast from the viewfinder and destroys any enemies it comes into contact with
    /// </summary>
    private void Shoot()
    {
        if (_bulletAmount >= 1)
        {
            Vector3 rayOrigin = _mainCamera.transform.position + _mainCamera.transform.up * aimOffset;

            Ray ray = new Ray(rayOrigin, _mainCamera.transform.forward);

            // Draw the bullet trajectory using Debug.DrawLine
            //Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red, 0.1f);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, shootDistance))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }

            SetBulletAmountText(1);
        }
    }

    
    /// <summary>
    /// Modifies the amount of bullets and displays it on the GUI
    /// </summary>
    /// <param name="reduceBulletAmount"></param>
    private void SetBulletAmountText(int reduceBulletAmount)
    {
        _bulletAmount -= reduceBulletAmount;
        bulletsLeftAmount.text = _bulletAmount.ToString();
    }
    
    /// <summary>
    /// Adds bullets periodically
    /// </summary>
    private void AddBullet()
    {
        _bulletAmount++;
        SetBulletAmountText(0);
    }

    private void DetectDeath()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, deathRadius, LayerMask.GetMask("Enemy"));

        if (colliders != null && colliders.Length > 0)
        {
            _gameController.EndGame();
            
            foreach (Collider collider in colliders)
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
