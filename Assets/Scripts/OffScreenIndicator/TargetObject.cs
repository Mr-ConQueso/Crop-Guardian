using UnityEngine;

public class TargetObject : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private IndicatorController _uiController;
    
    private void Awake() {
        _uiController = GetComponentInParent<IndicatorController>();
        if(_uiController == null)
        {
            _uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<IndicatorController>();
        }

        if (_uiController == null) Debug.LogError("No UIController component found");

        _uiController.AddTargetIndicator(gameObject);
    }

    private void OnDestroy()
    {
        if (gameObject != null)
        {
             _uiController.RemoveTargetIndicator(gameObject);
        }
    }
}