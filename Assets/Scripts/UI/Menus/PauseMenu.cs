using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        GameController.OnGamePaused += OnGamePaused;
        GameController.OnGameResumed += OnGameResumed;
    }
    
    private void OnDestroy()
    {
        GameController.OnGamePaused -= OnGamePaused;
        GameController.OnGameResumed -= OnGameResumed;
    }
    
    private void OnGameResumed()
    {
        GameController.Instance.CanPauseGame = false;
        _animator.SetTrigger("hideMenu");
    }

    private void OnGamePaused()
    {
        GameController.Instance.CanPauseGame = false;
        gameObject.SetActive(true);
        _animator.SetTrigger("showMenu");
    }
    
    public void OnClick_SettingsMenu()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
        GameController.Instance.SwitchPostProcessVolume(true);
    }

    public void OnClick_BackToMainMenu()
    {
        MenuManager.OpenMenu(Menu.ExitMenu, gameObject);
    }
    
    public void OnClick_ResumeGame()
    {
        GameController.Instance.InvokeOnGameResumed();
    }

    private void DisableAfterAnimation()
    {
        gameObject.SetActive(false);
    }
}
