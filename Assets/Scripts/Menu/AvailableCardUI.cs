using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AvailableCardUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private Color _availableColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.green;
    [SerializeField] private Color _lockedColor = Color.gray;

    [SerializeField] private CardSelecter _selecter;
    [SerializeField] private int _id;

    private CardStateType _currentState = CardStateType.None;
    public void Create(CardSelecter selecter, Card card, int id)
    {
        _selecter = selecter;
        _id = id;
        image.sprite = card.sprite;
        nameText.text = card.name;
        EditorUtility.SetDirty(this);
    }

    public void SetState(CardStateType state)
    {
        _currentState = state;
        switch (state)
        {
            case CardStateType.Available:
                nameText.color = _availableColor;
                break;
            case CardStateType.Selected:
                nameText.color = _selectedColor;
                break;
            case CardStateType.Locked:
                nameText.color = _lockedColor;
                break;
            default:
                nameText.color = Color.white;
                break;
        }
    }

    public void Click()
    {
        switch (_currentState)
        {
            case CardStateType.Available:
                _selecter.SelectCard(_id);
                SetState(CardStateType.Selected);
                break;
            case CardStateType.Selected:
                SetState(CardStateType.Available);
                break;
            case CardStateType.Locked:
                // Do nothing
                break;
            default:
                break;
        }
    }

    public enum CardStateType
    {
        None = 0,
        Available = 1,
        Selected = 2,
        Locked = 3
    }


}
