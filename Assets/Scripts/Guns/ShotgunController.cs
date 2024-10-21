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
            var mainCameraTransform = MainCameraTransform.transform;

            for (int i = 0; i < pelletsPerShot; i++)
            {
                // Calculate spread angle for each pellet
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);

                // Calculate ray direction based on spread angle
                Vector3 rayDirection = spreadRotation * mainCameraTransform.forward;

                // Create a ray from the camera position in the direction of the spread
                Ray ray = new Ray(mainCameraTransform.position, rayDirection);

                RaycastTrail(ray);
                
                DetectEnemiesKilled(ray);
            }

            // Update UI and perform other actions
            base.Shoot();
        }
    }
}