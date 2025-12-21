using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private List<int> _availableCards = new List<int>();
    [SerializeField]  private List<int> _selectedCards = new List<int>();

    private void Start()
    {
        StartLoad();
    }
    private void StartLoad()
    {
        Network.Instance.Post(UrlLibrary.MAIN + UrlLibrary.GETDECKINFO,
        new Dictionary<string, string> { { "userID", /* UserInfo.Instance.ID.ToString()*/"9" } },
        SuccessLoad,
        ErrorLoad);
    }

    private void ErrorLoad(string error)
    {
        Debug.LogError(error);
        StartLoad();
    }

    [SerializeField] private DeckData deckData;
    private void SuccessLoad(string data)
    {
        Debug.Log(data);
        deckData = JsonUtility.FromJson<DeckData>(data);
        for (int i = 0; i < deckData.selectedCardsIDs.Length; i++)
        {
            int.TryParse(deckData.selectedCardsIDs[i], out int selectedCardId);
            _selectedCards.Add(selectedCardId);     
        }

        for (int i = 0; i < deckData.availableCardsJson.Length; i++)
        {
            int.TryParse(deckData.availableCardsJson[i].id, out int id);
            _availableCards.Add(id);
        }

        _deckManager.Init(_availableCards, _selectedCards);
    }
}

[System.Serializable]
public class DeckData

{
    public Availablecardsjson[] availableCardsJson;
    public string[] selectedCardsIDs;
}

[System.Serializable]
public class Availablecardsjson
{
    public string name;
    public string id;

}



