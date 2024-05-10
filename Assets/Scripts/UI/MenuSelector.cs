using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private Selectable[] _selectableItems;
    private int _currentSelectionIndex;

    private void Awake()
    {
        // ---- / Get All The Selectable Items / ---- //
        _selectableItems = gameObject.GetComponentsInChildren<Selectable>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("SelectDown"))
        {
            SelectNextItem();
        }
        else if (Input.GetButtonDown("SelectUp"))
        {
            SelectPreviousItem();
        }
        ChangeSliderValue(_currentSelectionIndex, Input.GetAxis("Horizontal"));
        
        if (Input.GetButtonDown("Submit"))
        {
            ClickItem(_currentSelectionIndex);
        }
    }

    private void SelectNextItem()
    {
        _currentSelectionIndex = (_currentSelectionIndex + 1) % _selectableItems.Length;
    }
    
    private void SelectPreviousItem()
    {
        _currentSelectionIndex = (_currentSelectionIndex - 1 + _selectableItems.Length) % _selectableItems.Length;
    }

    private void ChangeSliderValue(int index, float amount)
    {
        if (_selectableItems[index] is Slider slider)
        {
            slider.value += amount / 100;
        }
    }

    private void ClickItem(int index)
    {
        if (_selectableItems[index] is Button button)
        {
            button.onClick.Invoke();
        }
    }
}
