using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ---- / Static Variables / ---- //
    public static bool IsDead;
    
    // ---- / Serialized Variables / ---- //
    [Header("Other")]
    [SerializeField] private float deathRadius;
    
    [Header("Guns")]
    [SerializeField] private float scrollDelay = 0.5f;
    [SerializeField] private GameObject[] gunsArray;
    
    [Header("Aiming")]
    [SerializeField] private  float aimTransitionDuration = 0.2f;
    [SerializeField] private float defaultFOV = 60.0f;
    [SerializeField] private float zoomedFOV = 30.0f;
    
    // ---- / Private Variables / ---- //
    private float _timeSinceLastShot;
    private Camera _mainCamera;
    private bool _isAiming;
    private float _aimTransitionStartTime;
    
    private int _currentWeapon = 0;
    private float _lastScrollTime;
    
    // ---- / Events / ---- //
    public delegate void DeathEventHandler();
    public static event DeathEventHandler OnPlayerDeath;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (IsDead == false && !UIController.Instance.IsGamePaused)
        {
            Aim();

            if (Time.time - _lastScrollTime > scrollDelay)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    _lastScrollTime = Time.time;
                    SwitchWeapon(1);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    _lastScrollTime = Time.time;
                    SwitchWeapon(-1);
                }
            }

            DetectDeath();   
        }
    }
    
    private void SwitchWeapon(int direction)
    {
        DeactivateGun(_currentWeapon);
        
        _currentWeapon += direction;
        _currentWeapon = Mathf.Clamp(_currentWeapon, 0, 4);
        
        ActivateGun(_currentWeapon);
        Debug.Log("Switched to weapon: " + _currentWeapon);
    }
    
    /// <summary>
    /// Modify the FOV when the user holds RMB.
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
    /// Check if any enemies are present in a
    /// sphere around the player, if so trigger the
    /// OnPlayerDeath? event.
    /// </summary>
    private void DetectDeath()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, deathRadius, LayerMask.GetMask("Enemy"));

        if (colliders is { Length: > 0 })
        {
            OnPlayerDeath?.Invoke();
            IsDead = true;
            
            foreach (Collider enemyCollider in colliders)
            {
                Destroy(enemyCollider.gameObject);
            }
        }
    }
    
    private void ActivateGun(int index)
    {
        if (index >= 0 && index < gunsArray.Length)
        {
            gunsArray[index].SetActive(true);
        }
    }

    private void DeactivateGun(int index)
    {
        if (index >= 0 && index < gunsArray.Length)
        {
            gunsArray[index].SetActive(false);
        }
    }
}
