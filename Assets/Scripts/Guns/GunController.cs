using System.Collections;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] protected int waitFrames = 0;
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected float reloadTime = 2f;
    [SerializeField] protected float fireRate = 0.1f;
    [SerializeField] protected int damageAmount = 1;
    [SerializeField] protected float shootDistance = 20f;
    [SerializeField] protected TextMeshProUGUI ammoText;
    [SerializeField] protected float textMoveAmount = 10f;
    
    [Header("Particles")]
    [SerializeField] protected TrailRenderer particleTrail;
    [SerializeField] protected Transform spawnTrailPosition;

    // ---- / Protected Variables / ---- //
    protected int CurrentAmmo;
    protected bool IsReloading = false;
    protected Camera MainCamera;
    protected float NextFireTime = 0f;
    
    // ---- / Private Variables / ---- //
    private Animator _animator;

    protected virtual void Start()
    {
        MainCamera = Camera.main;
        CurrentAmmo = maxAmmo;
        _animator = GetComponentInChildren<Animator>();
        
        UpdateAmmoUI();
    }

    protected virtual void Update()
    {
        if (IsReloading)
        {
            return;
        }

        if (CurrentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= NextFireTime && !UIController.Instance.IsGamePaused)
        {
            NextFireTime = Time.time + fireRate;
            StartShootAnimation();
        }
    }

    protected virtual IEnumerator Reload()
    {
        IsReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        CurrentAmmo = maxAmmo;
        IsReloading = false;
        UpdateAmmoUI();
    }

    protected virtual void StartShootAnimation()
    {
        _animator.SetTrigger("shoot");
        float timeTillShoot = waitFrames * (1.0f / 12.0f);
        Invoke(nameof(Shoot), timeTillShoot);
        StartCoroutine(MoveTextDownAndUp(8, 0.3f));
    }
    
    protected virtual void Shoot()
    {
        CurrentAmmo--;
        UpdateAmmoUI();
        
        var mainCameraTransform = MainCamera.transform;
        Vector3 rayOrigin = mainCameraTransform.position;

        Ray ray = new Ray(rayOrigin, mainCameraTransform.forward);

        DetectEnemiesKilled(ray);

        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance))
        {
            TrailRenderer trail = Instantiate(particleTrail, spawnTrailPosition.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
        }
    }

    protected virtual IEnumerator SpawnTrail(TrailRenderer trailRenderer, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trailRenderer.transform.position;

        while (time < 1)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trailRenderer.time;

            yield return null;
        }

        trailRenderer.transform.position = hit.point;
    }

    protected virtual void DetectEnemiesKilled(Ray ray)
    {
        // Draw the bullet trajectory using Debug.DrawLine
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red, 0.1f);
        
        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyController>().RemoveHealth(damageAmount);
            }
            
            if (hit.collider.CompareTag("Boss"))
            {
                hit.collider.gameObject.GetComponent<EnemyController>().RemoveHealth(damageAmount);
            }
        }
    }

    protected virtual void UpdateAmmoUI()
    {
        ammoText.text = CurrentAmmo.ToString();
    }

    protected virtual IEnumerator MoveTextDownAndUp(float animationSpeed, float pauseBetweenMove)
    {
        Vector3 originalPos = ammoText.rectTransform.localPosition;
        Vector3 targetPos = originalPos - Vector3.up * textMoveAmount;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed; // Adjust the speed of the animation
            ammoText.rectTransform.localPosition = Vector3.Lerp(originalPos, targetPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(pauseBetweenMove); // Adjust the pause between moves

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed; // Adjust the speed of the animation
            ammoText.rectTransform.localPosition = Vector3.Lerp(targetPos, originalPos, t);
            yield return null;
        }
    }
}
