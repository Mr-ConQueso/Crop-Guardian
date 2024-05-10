using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // ---- / Singleton / ---- //
    public static InputManager Instance;
    
    // ---- / Public Variables / ---- //
    public static PlayerInput PlayerInput { get; set; }
    public Vector2 NavigationInput { get; set;  }
    
    // ---- / Private Variables / ---- //
    private InputAction _navigationAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();
        _navigationAction = PlayerInput.actions["Navigate"];
    }

    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();
    }
}
