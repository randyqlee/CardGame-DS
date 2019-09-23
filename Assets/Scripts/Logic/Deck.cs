using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    public List<CardAsset> cards;

    void Awake()
    {
        //cards.Shuffle();
        if (BattleStartInfo.SelectedDeck!=null && GetComponentInParent<Player>().PArea.owner == AreaPosition.Low)
        {
            for (int i = cards.Count -1 ; i >= 0 ; i--)
            {
                cards.Remove(cards[i]);
            }
            

            foreach (CardAsset ca in BattleStartInfo.SelectedDeck.Cards)
            {
                cards.Add(ca);
            }

        }

        if (BattleStartInfo.EnemyDeck!=null && GetComponentInParent<Player>().PArea.owner == AreaPosition.Top)
        {
            for (int i = cards.Count -1 ; i >= 0 ; i--)
            {
                cards.Remove(cards[i]);
            }
            

            foreach (CardAsset ca in BattleStartInfo.EnemyDeck.Cards)
            {
                cards.Add(ca);
            }

        }
    }
	
}
