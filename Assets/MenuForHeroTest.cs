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

    }
}
