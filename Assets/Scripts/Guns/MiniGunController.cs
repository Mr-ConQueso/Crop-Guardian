using System.Collections;
using UnityEngine;

namespace Player.Guns
{
    public class MiniGunController : GunController
    {
        // ---- / Serialized Variables / ---- //
        [SerializeField] private float delayPerShots = 0.2f;
        [SerializeField] private int totalShots = 3;
        
        // ---- / Private Variables / ---- //
        private int _currentShots;

        protected override void StartShootAnimation()
        {
            Animator.SetTrigger(AnimatorShootTrigger);
            StartCoroutine(ShootMultipleTimes(totalShots, delayPerShots));  // Example: Shoot 3 times with a 0.5s delay
            StartCoroutine(MoveTextDownAndUp(8, 0.3f));
        }

        private IEnumerator ShootMultipleTimes(int shotCount, float delayBetweenShots)
        {
            for (int i = 0; i < shotCount; i++)
            {
                float timeTillShoot = waitFrames * (1.0f / 12.0f);
                yield return new WaitForSeconds(timeTillShoot);
                Shoot();

                if (i < shotCount - 1)
                {
                    yield return new WaitForSeconds(delayBetweenShots);
                }
            }
        }
    }
}