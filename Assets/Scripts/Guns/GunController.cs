using System.Collections;
using Health;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] protected int waitFrames;
    [SerializeField] protected float fireRate = 0.1f;
    [SerializeField] protected float damageAmount = 0.5f;
    
    [Header("Ammo")]
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected float reloadTime = 2f;
    [SerializeField] protected float shootDistance = 20f;
    
    [Header("Ammo Text")]
    [SerializeField] protected TextMeshProUGUI ammoText;
    [SerializeField] protected float textMoveAmount = 10f;
    
    [Header("Ray")]
    [SerializeField] protected Transform startRayPosition;
    [SerializeField] protected Transform endRayPosition;
    
    [Header("Particles")]
    [SerializeField] protected ParticleSystem spawnParticle;
    [SerializeField] protected ParticleSystem impactParticle;
    [SerializeField] protected TrailRenderer bulletTrail;

    // ---- / Protected Variables / ---- // 
    protected int CurrentAmmo;
    protected bool IsReloading;
    protected Transform MainCameraTransform;
    protected float NextFireTime;
    protected Vector3 rayDirection;
    protected Animator Animator;
    protected static readonly int AnimatorShootTrigger = Animator.StringToHash("shoot");
    
    protected virtual void Start()
    {
        if (Camera.main != null) MainCameraTransform = Camera.main.transform;
        CurrentAmmo = maxAmmo;
        Animator = GetComponentInChildren<Animator>();
        
        UpdateAmmoUI(); 
    }

    protected virtual void Update()
    {
        rayDirection = (endRayPosition.position - startRayPosition.position).normalized;

        if (IsReloading)
        {
            return;
        }

        if (CurrentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (InputManager.WasAttackPressed && Time.time >= NextFireTime && !(GameController.Instance.IsGamePaused || GameController.Instance.IsPlayerFrozen))
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
        Animator.SetTrigger(AnimatorShootTrigger);
        float timeTillShoot = waitFrames * (1.0f / 12.0f);
        Invoke(nameof(Shoot), timeTillShoot);
        StartCoroutine(MoveTextDownAndUp(8, 0.3f));
    }
    
    protected virtual void Shoot()
    {
        CurrentAmmo--;
        UpdateAmmoUI();
        
        Vector3 rayOrigin = MainCameraTransform.position;

        Ray ray = new Ray(startRayPosition.position, rayDirection);
        Debug.DrawLine(startRayPosition.position, rayDirection * 100, Color.magenta, 5f);
        
        RaycastTrail(ray);

        DetectEnemiesKilled(ray);
    }

    protected virtual void RaycastTrail(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance))
        {
            TrailRenderer trail = Instantiate(bulletTrail, startRayPosition.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
        }
        else
        {
            Vector3 noHitEndPoint = rayDirection * shootDistance;
            TrailRenderer trail = Instantiate(bulletTrail, startRayPosition.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, noHitEndPoint, Vector3.zero, false));
        }
    }

    protected virtual IEnumerator SpawnTrail(TrailRenderer trailRenderer, Vector3 endPoint, Vector3 normal, bool hasHit)
    {
        float time = 0;
        Vector3 startPosition = trailRenderer.transform.position;

        while (time < 1)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPosition, endPoint, time);
            time += Time.deltaTime / trailRenderer.time;

            yield return null;
        }

        trailRenderer.transform.position = endPoint;

        if (hasHit)
        {
            Instantiate(impactParticle, endPoint, Quaternion.LookRotation(normal));
        }
        
        Destroy(trailRenderer.GameObject(), trailRenderer.time);
    }

    protected virtual void DetectEnemiesKilled(Ray ray)
    {
        // Draw the bullet trajectory using Debug.DrawLine
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red, 0.1f);
        
        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            if ((hitObject.CompareTag("Enemy") || hitObject.CompareTag("Boss")) 
                    && hitObject.TryGetComponent(out IDamageable iDamageable))
            {
                iDamageable.RemoveHealth(damageAmount);
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
