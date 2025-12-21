using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();

    public IReadOnlyList<Card> AvailableCards => _availableCards;
    public IReadOnlyList<Card> SelectedCards => _selectedCards;

    public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> OnUpdateAvailableCards;
    public event Action<IReadOnlyList<Card>> OnUpdateSelectedCards;

    #region Editor
#if UNITY_EDITOR
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    private void OnValidate()
    {
        _availableDeckUI.SetAllCardsCount(_cards);
    }
#endif
    #endregion

    public void Init(List<int> availableCardIds, List<int> selectedCardIds)
    {
        Debug.Log("DeckManager Init called with available cards: " + string.Join(",", availableCardIds) + " and selected cards: " + string.Join(",", selectedCardIds));
        foreach (var cardId in availableCardIds)
        {
            _availableCards.Add(_cards[cardId]);

        }

        foreach (var cardId in selectedCardIds)
        {
            _selectedCards.Add(_cards[cardId]);

        }
        OnUpdateAvailableCards?.Invoke(AvailableCards, SelectedCards);
        OnUpdateSelectedCards?.Invoke(SelectedCards);
    }
}



[System.Serializable]
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public int id { get; private set; }
}
