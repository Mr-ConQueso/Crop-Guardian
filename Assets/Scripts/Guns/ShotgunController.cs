using UnityEngine;

namespace Player.Guns
{
    public class ShotgunController : GunController
    {
        // ---- / Serialized Variables / ---- //
        [SerializeField] protected int pelletsPerShot = 5;
        [SerializeField] protected float spreadAngle = 20f;
        
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