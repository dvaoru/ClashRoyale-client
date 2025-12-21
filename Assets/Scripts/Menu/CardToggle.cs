using UnityEngine;

public class CardToggle : MonoBehaviour
{
    [SerializeField] private CardSelecter _cardSelecter;
    [SerializeField] private int index;

    public void Click(bool value)
    {
        if (value == false) return;
        _cardSelecter.SelectToggleIndex(index);
    }
}
