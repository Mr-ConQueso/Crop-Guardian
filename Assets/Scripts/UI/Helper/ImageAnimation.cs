using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {
    
    // ---- / Serialized Variables / ---- //
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected int spritePerFrame = 6;
    [SerializeField] protected bool loop = true;
    [SerializeField] protected bool destroyOnEnd;
    
    // ---- / Private Variables / ---- //
    private int _index;
    private Image _image;
    private int _frame;
    private bool _isAnimationPlaying;

    public void StartAnimation()
    {
        _isAnimationPlaying = true;
    }
    
    protected virtual void Awake() {
        _image = GetComponent<Image>();
    }

    protected virtual void Update ()
    {
        if (_isAnimationPlaying)
        {
            UpdateAnimation();
        }
    }

    protected virtual void UpdateAnimation()
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
            EndAnimation();
        }
    }

    protected virtual void EndAnimation()
    {
        _isAnimationPlaying = false;
        _index = 0;
        _frame = 0;
    }
}