using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject endGameScreen;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        
        endGameScreen.SetActive(false);
    }

    public void EndGame()
    {
        endGameScreen.SetActive(true);
    }
}
