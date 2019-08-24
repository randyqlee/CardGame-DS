using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    public List<CardAsset> cards = new List<CardAsset>();

    void Awake()
    {
        //cards.Shuffle();
        if (BattleStartInfo.SelectedDeck!=null && GetComponentInParent<Player>().PArea.owner == AreaPosition.Low)
        {
            foreach (CardAsset ca in BattleStartInfo.SelectedDeck.Cards)
            {
                cards.Add(ca);
            }

        }
    }
	
}
