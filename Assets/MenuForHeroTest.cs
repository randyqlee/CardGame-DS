using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuForHeroTest : MonoBehaviour
{
    public DeckBuilderTest TOP_DeckBuildingScreen;
    public DeckBuilderTest LOW_DeckBuildingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CopyDeckInfo()
    {
        BattleStartInfo.EnemyDeck = new DeckInfo(TOP_DeckBuildingScreen.deckList);
        BattleStartInfo.SelectedDeck = new DeckInfo(LOW_DeckBuildingScreen.deckList);

        if (BattleStartInfo.SelectedDeck.Cards.Count == 0)
        {
            CardAsset[] allCardsArray = Resources.LoadAll<CardAsset>("");
            List<CardAsset> allCardsList = new List<CardAsset>(allCardsArray);
            for (int i = 0; i < 4; i ++)
            {
                int index = Random.Range(0, allCardsList.Count);
                BattleStartInfo.SelectedDeck.Cards.Add(allCardsList[index]);

                Debug.Log ("AI Random Pick " + i + ": " + allCardsList[index].name);
                allCardsList.RemoveAt(index);
            }
        }

        if (BattleStartInfo.EnemyDeck.Cards.Count == 0)
        {
            CardAsset[] allCardsArray = Resources.LoadAll<CardAsset>("");
            List<CardAsset> allCardsList = new List<CardAsset>(allCardsArray);
            for (int i = 0; i < 4; i ++)
            {
                int index = Random.Range(0, allCardsList.Count);
                BattleStartInfo.EnemyDeck.Cards.Add(allCardsList[index]);

                Debug.Log ("AI Random Pick " + i + ": " + allCardsList[index].name);
                allCardsList.RemoveAt(index);
            }
        }

    }
}
