using System.Collections;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected float reloadTime = 2f;
    [SerializeField] protected float fireRate = 0.1f;
    [SerializeField] protected float shootDistance = 20f;
    [SerializeField] protected TextMeshProUGUI ammoText;
    [SerializeField] protected float textMoveAmount = 10f;

    // ---- / Protected Variables / ---- //
    protected int CurrentAmmo;
    protected bool IsReloading = false;
    protected Camera MainCamera;
    protected float NextFireTime = 0f;

    void Start()
    {
        MainCamera = Camera.main;
        CurrentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
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

        if (Input.GetButton("Fire1") && Time.time >= NextFireTime)
        {
            NextFireTime = Time.time + fireRate;
            Shoot();
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
    
    protected virtual void Shoot()
    {
        CurrentAmmo--;
        UpdateAmmoUI();
        
        var mainCameraTransform = MainCamera.transform;
        Vector3 rayOrigin = mainCameraTransform.position;

        Ray ray = new Ray(rayOrigin, mainCameraTransform.forward);

        DetectEnemiesKilled(ray);
        
        StartCoroutine(MoveTextDownAndUp());
    }

    protected virtual void DetectEnemiesKilled(Ray ray)
    {
        // Draw the bullet trajectory using Debug.DrawLine
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red, 0.1f);
        
        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyController>().RemoveHealth(1);
            }
            
            if (hit.collider.CompareTag("Boss"))
            {
                hit.collider.gameObject.GetComponent<EnemyController>().RemoveHealth(1);
            }
        }
    }

    protected virtual void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + CurrentAmmo + "/" + maxAmmo;
    }

    protected virtual IEnumerator MoveTextDownAndUp()
    {
        Vector3 originalPos = ammoText.rectTransform.localPosition;
        Vector3 targetPos = originalPos - Vector3.up * textMoveAmount;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2; // Adjust the speed of the animation
            ammoText.rectTransform.localPosition = Vector3.Lerp(originalPos, targetPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // Adjust the pause between moves

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2; // Adjust the speed of the animation
            ammoText.rectTransform.localPosition = Vector3.Lerp(targetPos, originalPos, t);
            yield return null;
        }
    }
}
