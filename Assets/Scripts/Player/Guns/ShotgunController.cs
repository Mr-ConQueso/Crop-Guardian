using UnityEngine;

namespace Player.Guns
{
    public class ShotgunController : GunController
    {
        // ---- / Serialized Variables / ---- //
        [SerializeField] private int pelletsPerShot = 5;
        [SerializeField] private float spreadAngle = 20f;

        void Start()
        {
            MainCamera = Camera.main;
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
        
        protected override void Shoot()
        {
            for (int i = 0; i < pelletsPerShot; i++)
            {
                // Calculate spread angle for each pellet
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);

                // Calculate ray direction based on spread angle
                Vector3 rayDirection = spreadRotation * MainCamera.transform.forward;

                // Create a ray from the camera position in the direction of the spread
                Ray ray = new Ray(MainCamera.transform.position, rayDirection);

                // Detect enemies killed by this pellet
                DetectEnemiesKilled(ray);
            }

            // Update UI and perform other actions
            base.Shoot();
        }
    }
}