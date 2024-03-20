using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private void Awake()
    {
        UIController uiController = GetComponentInParent<UIController>();
        if(uiController == null)
        {
            uiController = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
        }

        if (uiController == null) Debug.LogError("No UIController component found");

        uiController.AddTargetIndicator(this.gameObject);
    }
}