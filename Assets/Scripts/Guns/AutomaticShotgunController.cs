using UnityEngine;

namespace Player.Guns
{
    public class AutomaticShotgunController : ShotgunController
    {
        protected override void Shoot()
        {
            CurrentAmmo--;
            UpdateAmmoUI();
        
            var mainCameraTransform = MainCamera.transform;
            Vector3 rayOrigin = mainCameraTransform.position;

            Ray ray = new Ray(rayOrigin, mainCameraTransform.forward);

            DetectEnemiesKilled(ray);
        }
    }
}