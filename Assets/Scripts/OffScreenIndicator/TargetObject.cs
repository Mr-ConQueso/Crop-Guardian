using UnityEngine;

public class TargetObject : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private UIController _uiController;
    
    private void Awake()
    {
        _uiController = GetComponentInParent<UIController>();
        if(_uiController == null)
        {
            _uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
        }

        if (_uiController == null) Debug.LogError("No UIController component found");

        _uiController.AddTargetIndicator(this.gameObject);
    }

    private void OnDestroy()
    {
        _uiController.RemoveTargetIndicator(this.gameObject);
    }
}