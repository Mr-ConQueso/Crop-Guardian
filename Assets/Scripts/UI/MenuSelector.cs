using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private Vector3 selectorOffset;
    [SerializeField] private bool isCameraOverlay = true;
    
    // ---- / Private Variables / ---- //
    private RectTransform _selector;
    private Selectable[] _selectableItems;
    private int _currentSelectionIndex;

    private void Awake()
    {
        // ---- / Get All The Selectable Items / ---- //
        _selectableItems = gameObject.GetComponentsInChildren<Selectable>();

        // ---- / Get The Selector / ---- //
        _selector = FindChildWithTag(gameObject.transform, "ButtonSelector").GetComponent<RectTransform>();

        SetPosition(_currentSelectionIndex);
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
        SetPosition(_currentSelectionIndex);
    }
    
    private void SelectPreviousItem()
    {
        _currentSelectionIndex = (_currentSelectionIndex - 1 + _selectableItems.Length) % _selectableItems.Length;
        SetPosition(_currentSelectionIndex);
    }

    private void ChangeSliderValue(int index, float amount)
    {
        if (_selectableItems[index] is Slider slider)
        {
            slider.value += amount / 100;
        }
    }

    private void SetPosition(int index)
    {
        var finalOffset = selectorOffset - new Vector3(_selectableItems[index].GetComponent<RectTransform>().rect.width / GetCameraOffset(), 0, 0);
        _selector.position = _selectableItems[index].transform.position + finalOffset;
        
        _selectableItems[index].Select();
    }

    private void ClickItem(int index)
    {
        if (_selectableItems[index] is Button button)
        {
            button.onClick.Invoke();
        }
    }
    
    private static Transform FindChildWithTag(Component root, string tag)
    {
        return root.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.CompareTag(tag));
    }

    private int GetCameraOffset()
    {
        return isCameraOverlay switch
        {
            true => 2,
            false => 16
        };
    }
}
