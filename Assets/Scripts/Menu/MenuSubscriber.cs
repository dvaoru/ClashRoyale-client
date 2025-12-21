using UnityEngine;

public class MenuSubscriber : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;

    [SerializeField] private SelectedDeckUI _selectedDeckUI2;
    [SerializeField] private AvailableDeckUI _availableDeckUI;

    void Start()
    {
        _deckManager.OnUpdateSelectedCards += _selectedDeckUI.UpdateCardsList;
        _deckManager.OnUpdateSelectedCards += _selectedDeckUI2.UpdateCardsList;
        _deckManager.OnUpdateAvailableCards += _availableDeckUI.UpdateCardsList;
    }

    void OnDestroy()
    {
        _deckManager.OnUpdateSelectedCards -= _selectedDeckUI.UpdateCardsList;
        _deckManager.OnUpdateSelectedCards -= _selectedDeckUI2.UpdateCardsList;
        _deckManager.OnUpdateAvailableCards -= _availableDeckUI.UpdateCardsList;
    }
}
