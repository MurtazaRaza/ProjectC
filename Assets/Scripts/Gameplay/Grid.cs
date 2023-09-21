using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityUtilities;

public class Grid : MonoBehaviour
{
    [SerializeField] 
    private GridLayoutGroup gridLayoutGroup;

    [FormerlySerializedAs("gridData")] 
    [SerializeField]
    private GridData[] gridDatas;

    [SerializeField] 
    private List<CardData> CardDatas;

    [SerializeField] 
    private Card cardPrefab;

    private List<Card> _cardGameObjects = new List<Card>();
    
    public void InitializeGrid(int numberOfCards, int numberOfCardsToSelect)
    {
        SetupGridLayout(numberOfCards);

        List<CardData> listSubsection = new List<CardData>();
        GetSubsectionOfShuffledListToPopulateWith(numberOfCards, numberOfCardsToSelect, ref listSubsection);

        for (int i = 0; i < numberOfCardsToSelect; i++)
        {
            for (int n = 0; n < listSubsection.Count; n++)
            {
                var card = Instantiate(cardPrefab.gameObject, gridLayoutGroup.transform).GetComponent<Card>();
                card.PopulateCard(listSubsection[n]);
                card.gameObject.name = card.CardData.cardName;
                _cardGameObjects.Add(card);
            }
            
            listSubsection.Shuffle();
        }
    }

    private void GetSubsectionOfShuffledListToPopulateWith(int numberOfCards, int numberOfCardsToSelect, ref List<CardData> listSubsection)
    {
        if (numberOfCards % numberOfCardsToSelect != 0)
        {
            Debug.LogError("Number of cards with number of cards to select can never complete");
            throw new Exception("number of cards and number of cards to select mismatch");
        }
        
        List<CardData> cardDatasToManipulate = new List<CardData>(CardDatas);
        cardDatasToManipulate.Shuffle();
        
        listSubsection = cardDatasToManipulate.GetRange(0, (numberOfCards / numberOfCardsToSelect));
    }

    private void SetupGridLayout(int numberOfCards)
    {
        GridData gridData = GetGridDataAccordingToNumberOfCards(numberOfCards);
        gridLayoutGroup.cellSize = gridData.cardSize;
        gridLayoutGroup.constraintCount = gridData.numberOfRows;
        gridLayoutGroup.spacing = gridData.spacing;
    }

    // TODO add deserialized parameters to initialize grid from saved values
    public void ReInitializeGrid()
    {

        throw new NotImplementedException();
    }

    private GridData GetGridDataAccordingToNumberOfCards(int numberOfCards)
    {
        GridData gridDataToUse = gridDatas[0];
        if (numberOfCards <= gridDataToUse.maxCardsThatCanFit)
            return gridDataToUse;
        
        for (var index = 1; index < gridDatas.Length; index++)
        {
            var gridData = gridDatas[index];
            if (numberOfCards < gridData.maxCardsThatCanFit)
                return gridData;
        }

        throw new Exception("Number of cards greater than what is supported");
    }
}
