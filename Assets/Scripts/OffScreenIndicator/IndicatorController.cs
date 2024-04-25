using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<TargetIndicator> targetIndicators = new List<TargetIndicator>();
    [SerializeField] private GameObject targetIndicatorPrefab;
    [SerializeField] private Transform parentFolder;
    
    // ---- / Private Variables / ---- //
    private Camera _mainCamera;
    
    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeathHandler;
    }

    /// <summary>
    /// Create an off-screen target indicator to the
    /// canvas and add it to the list.
    /// </summary>
    /// <param name="target"></param>
    public void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = Instantiate(targetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, _mainCamera, canvas);
        indicator.transform.SetParent(parentFolder);
        targetIndicators.Add(indicator);
    }

    public void RemoveTargetIndicator(GameObject target)
    {
        for (int i = 0; i < targetIndicators.Count; i++)
        {
            if (targetIndicators[i].GetTarget() == target)
            {
                targetIndicators.RemoveAt(i);
                break;
            }
        }
    }
    
    private void Start()
    {
        _mainCamera = Camera.main;
        PlayerController.OnPlayerDeath += OnPlayerDeathHandler;
    }
    
    private void Update()
    {
        if (targetIndicators.Count <= 0) return;
        foreach (var t in targetIndicators)
        {
            t.UpdateTargetIndicator();
        }
    }
    
    private void OnPlayerDeathHandler()
    {
        foreach (var indicator in targetIndicators)
        {
            if (indicator != null)
            {
                Destroy(indicator.gameObject);
            }
        }

        targetIndicators.Clear();
    }
}