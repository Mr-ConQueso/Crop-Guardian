using UnityEngine;

public class TargetObject : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private IndicatorController _indicatorController;
    
    private void Awake()
    {
        _indicatorController = GetComponentInParent<IndicatorController>();
        if(_indicatorController == null)
        {
            _indicatorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<IndicatorController>();
        }

        if (_indicatorController == null) Debug.LogError("No IndicatorController component found");

        _indicatorController.AddTargetIndicator(this.gameObject);
    }

    private void OnDestroy()
    {
        _indicatorController.RemoveTargetIndicator(this.gameObject);
    }
}