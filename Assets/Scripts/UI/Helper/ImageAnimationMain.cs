using BaseGame;

public class ImageAnimationMain : ImageAnimation {
    
    // ---- / Private Variables / ---- //
    private SceneTransitionManager _sceneTransitionManager;
    
    protected override void Awake() {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        base.Awake();
    }

    protected override void EndAnimation()
    {
        _sceneTransitionManager.EndLoadIn();
        base.EndAnimation();
    }
}