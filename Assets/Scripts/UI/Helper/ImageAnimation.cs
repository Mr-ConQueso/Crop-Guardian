using BaseGame;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {

    // ---- / Serialized Variables / ---- //
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int spritePerFrame = 6;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool destroyOnEnd = false;
    
    // ---- / Private Variables / ---- //
    private SceneTransitionManager _sceneTransitionManager;
    private int _index = 0;
    private Image _image;
    private int _frame = 0;
    private bool _isAnimationPlaying;

    public void StartAnimation()
    {
        _isAnimationPlaying = true;
    }
    
    private void Awake() {
        _image = GetComponent<Image>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
    }

    private void Update ()
    {
        if (_isAnimationPlaying)
        {
            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        if (!loop && _index == sprites.Length) return;
        _frame ++;
        if (_frame < spritePerFrame) return;
        _image.sprite = sprites [_index];
        _frame = 0;
        _index ++;
        if (_index >= sprites.Length) {
            if (loop) _index = 0;
            if (destroyOnEnd) Destroy (gameObject);
            _isAnimationPlaying = false;
            _sceneTransitionManager.EndLoadIn();
            _index = 0;
            _frame = 0;
        }
    }
}