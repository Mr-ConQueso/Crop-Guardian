using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        animator.SetTrigger("showMenu");
    }
    
    public void OnClick_SettingsMenu()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
        GameController.Instance.SwitchVFXVolume(true);
    }

    public void OnClick_BackToMainMenu()
    {
        MenuManager.OpenMenu(Menu.ExitMenu, gameObject);
    }
    
    public void OnClick_BackToGame()
    {
        animator.SetTrigger("hideMenu");
    }

    private void DisableAfterAnimation()
    {
        gameObject.SetActive(false);
        UIController.Instance.UnPauseGame();
    }
}
