using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<TargetIndicator> targetIndicators = new List<TargetIndicator>();
    [SerializeField] private GameObject targetIndicatorPrefab;
    
    // ---- / Private Variables / ---- //
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = Instantiate(targetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, _mainCamera, canvas);
        targetIndicators.Add(indicator);
    }
    
    private void Update()
    {
        if(targetIndicators.Count > 0)
        {
            for(int i = 0; i < targetIndicators.Count; i++)
            {
                targetIndicators[i].UpdateTargetIndicator();
            }
        }
    }
}