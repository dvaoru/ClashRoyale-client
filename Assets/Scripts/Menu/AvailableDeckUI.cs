using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AvailableDeckUI : MonoBehaviour
{
    [SerializeField] private CardSelecter _cardSelecter;
    [SerializeField] private List<AvailableCardUI> _availableCards = new List<AvailableCardUI>();
    #region Editor
#if UNITY_EDITOR
    [SerializeField] private Transform _content;
    [SerializeField] private AvailableCardUI _availableCardPrefab;

    public void SetAllCardsCount(Card[] cards)
    {

        for (int i = 0; i < _availableCards.Count; i++)
        {
            GameObject go = _availableCards[i].gameObject;
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(go);

        }
        _availableCards.Clear();
        for (int i = 0; i < cards.Length; i++)
        {
            var cardUI = Instantiate(_availableCardPrefab, _content);
            cardUI.Create(_cardSelecter,cards[i], i);
            _availableCards.Add(cardUI);
          
        }
    }
#endif
    #endregion

    public void UpdateCardsList(IReadOnlyList<Card> available, IReadOnlyList<Card> selected)
    {
        for (int i = 0; i < _availableCards.Count; i++)
        {
            _availableCards[i].SetState(AvailableCardUI.CardStateType.Locked);
        }

        for (int i = 0; i < available.Count; i++)
        {
            _availableCards[available[i].id - 1].SetState(AvailableCardUI.CardStateType.Available);
        }

        for (int i = 0; i < selected.Count; i++)
        {
            _availableCards[selected[i].id - 1].SetState(AvailableCardUI.CardStateType.Selected);
        }
    }
}
