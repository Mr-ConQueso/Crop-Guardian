using UnityEngine;

namespace BaseGame
{
    public class SceneTransitionManager : MonoBehaviour
    {
        // ---- / Singleton / ---- //
        public static SceneTransitionManager Instance;
        
        // ---- / Public Variables / ---- //
        public bool IsFadingIn { get; private set; }
        public bool IsFadingOut { get; private set; }
        
        // ---- / Serialized Variables / ---- //
        [SerializeField] private Animator transitionAnimator;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void EndLoadIn()
        {
            IsFadingIn = false;
        }
        
        public void EndLoadOut()
        {
            IsFadingOut = false;
        }

        public void StartAnimation()
        {
            transitionAnimator.SetTrigger("triggerStart");
            IsFadingIn = true;
        }

        public void EndAnimation()
        {
            transitionAnimator.SetTrigger("triggerEnd");
            IsFadingOut = true;
        }
    }
}