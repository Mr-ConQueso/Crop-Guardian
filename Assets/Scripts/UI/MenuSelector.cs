using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private RectTransform _selector;
    private readonly Vector3 _selectorOffset = new (-60, 15, 0);
    private Button[] _selectableButtons;

    private int _currentSelectionIndex;
    private float[] _selectableButtonsWidth;

    private void Awake()
    {
        // ---- / Get All The Buttons / ---- //
        _selectableButtons = gameObject.GetComponentsInChildren<Button>();
        
        // ---- / Get The Selector / ---- //
        _selector = FindChildWithTag(gameObject.transform, "ButtonSelector").GetComponent<RectTransform>();
        
        SetPosition(_currentSelectionIndex);
        /*
        for (int i = 0; i < selectableButtons.Length; i++)
        {
            if (selectableButtons[i].TryGetComponent<RectTransform>(out RectTransform buttonRect))
            {
                _selectableButtonsWidth[i] = buttonRect.rect.width;
            }
            else
            {
                Debug.LogWarning("Selectable button at index " + i + " is not assigned.");
            }
        }
        */
    }
    /*
    private void OnEnable()
    {
        _currentSelectionIndex = 0;
        SetPosition(_currentSelectionIndex);
    }
    */

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SelectNextButton();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            SelectPreviousButton();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ClickButton(_currentSelectionIndex);
        }
    }

    private void SelectNextButton()
    {
        if (_currentSelectionIndex < _selectableButtons.Length -1)
        {
            _currentSelectionIndex++;
            SetPosition(_currentSelectionIndex);
        }
        else
        {
            _currentSelectionIndex = 0;
            SetPosition(_currentSelectionIndex);
        }
    }
    
    private void SelectPreviousButton()
    {
        if (_currentSelectionIndex > 0)
        {
            _currentSelectionIndex--;
            SetPosition(_currentSelectionIndex);
        }
        else
        {
            _currentSelectionIndex = _selectableButtons.Length -1;
            SetPosition(_currentSelectionIndex);
        }
    }

    private void SetPosition(int index)
    {
        var finalOffset = _selectorOffset - new Vector3(_selectableButtons[index].GetComponent<RectTransform>().rect.width / 2, 0, 0);
        _selector.position = _selectableButtons[index].transform.position + finalOffset;
        
        _selectableButtons[index].Select();
        //EventSystem.current.SetSelectedGameObject(selectableButtons[index].gameObject, new BaseEventData(EventSystem.current));
    }

    private void ClickButton(int index)
    {
        _selectableButtons[index].onClick.Invoke();
    }
    
    private Transform FindChildWithTag(Transform root, string tag)
    {
        return root.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.CompareTag(tag));
    }
}
