using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedDeckUI : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private Image[] _images;

    public void UpdateCardsList(IReadOnlyList<Card> cards)
    {
        foreach (var card in cards)
        {
            Debug.Log("Card in SelectedDeckUI: " + card.name + " with ID: " + card.id);
        }

        for (int i = 0; i < _images.Length; i++)
        {
            if (i < cards.Count)
            {
                _images[i].sprite = cards[i].sprite;
                _images[i].enabled = true;
            }
            else
            {
                _images[i].sprite = null;
                _images[i].enabled = false;
            }
        }
    }
}
