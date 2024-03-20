using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private void Awake()
    {
        UIController ui = GetComponentInParent<UIController>();
        if(ui == null)
        {
            ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}