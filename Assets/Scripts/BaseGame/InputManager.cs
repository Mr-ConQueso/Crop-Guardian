using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // ---- / Singleton / ---- //
    public static InputManager Instance;
    
    // ---- / Public Variables / ---- //
    public Vector2 NavigationInput { get; set; }
    
    public static double LookHorizontalInput;
    public static double LookVerticalInput;

    public static bool WasAttackPressed;
    public static bool WasInteractPressed;
    
    // ---- / Private Variables / ---- //
    private InputAction _lookHorizontalAction;
    private InputAction _lookVerticalAction;
    
    private InputAction _attackAction;
    private InputAction _interactAction;
    
    private InputAction _navigationAction;
    private static PlayerInput _playerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        
        _navigationAction = _playerInput.actions["Navigate"];
        _lookHorizontalAction = _playerInput.actions["LookHorizontal"];
        _lookVerticalAction = _playerInput.actions["LookVertical"];
        
        _attackAction = _playerInput.actions["Fire"];
        _interactAction = _playerInput.actions["Interact"];
    }

    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();
        
        //LookHorizontalInput = _lookHorizontalAction.ReadValue<double>();
        //LookVerticalInput = _lookVerticalAction.ReadValue<double>();

        WasAttackPressed = _attackAction.WasPressedThisFrame();
        WasInteractPressed = _interactAction.WasPressedThisFrame();
    }
}
