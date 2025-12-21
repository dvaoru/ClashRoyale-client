using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;
    private List<Card> _availableCards = new List<Card>();
    private List<Card> _selectedCards = new List<Card>();

    public IReadOnlyList<Card> AvailableCards => _availableCards;
    public IReadOnlyList<Card> SelectedCards => _selectedCards;

    private int _selectedToggleIndex;

    private void OnEnable()
    {
        _availableCards.Clear();
        for (int i = 0; i < _deckManager.AvailableCards.Count; i++)
        {
            _availableCards.Add(_deckManager.AvailableCards[i]);
        }

        _selectedCards.Clear();
        for (int i = 0; i < _deckManager.SelectedCards.Count; i++)
        {
            _selectedCards.Add(_deckManager.SelectedCards[i]);
        }
    }
    internal void SelectToggleIndex(int index)
    {
        _selectedToggleIndex = index;
    }

    public void SelectCard(int cardid)
    {
        _selectedCards[_selectedToggleIndex] = _availableCards[cardid - 1];
        _selectedDeckUI.UpdateCardsList(SelectedCards);
        _availableDeckUI.UpdateCardsList(AvailableCards, SelectedCards);
    }

}
