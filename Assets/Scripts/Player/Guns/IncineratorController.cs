using UnityEngine;

namespace Player.Guns
{
    public class IncineratorController : GunController
    {
        // ---- / Serialized Variables / ---- //

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
            base.Shoot();
        }
    }
}