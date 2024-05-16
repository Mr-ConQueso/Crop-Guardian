using System;
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
        
        // ---- / Private Variables / ---- //
        private Animator _transitionAnimator;
        private ImageAnimation _imageAnimation;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _transitionAnimator = GetComponent<Animator>();
            _imageAnimation = GetComponent<ImageAnimation>();
        }

        public void EndLoadIn()
        {
            IsFadingIn = false;
            EndAnimation();
        }
        
        public void EndLoadOut()
        {
            IsFadingOut = false;
        }

        public void StartAnimation()
        {
            _imageAnimation.StartAnimation();
            _transitionAnimator.SetTrigger("triggerStart");
            IsFadingIn = true;
        }

        public void EndAnimation()
        {
            _transitionAnimator.SetTrigger("triggerEnd");
            IsFadingOut = true;
        }
    }
}