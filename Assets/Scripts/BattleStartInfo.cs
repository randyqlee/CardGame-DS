using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStartInfo
{
    public List<CardAsset> deckList;
    public CharacterAsset playerClass;
}

public enum GameMode
{
    PvP,
    PvAI,
    AIvAI
}

public static class BattleStartInfo  
{
    public static PlayerStartInfo[] PlayerInfos; 

    public static DeckInfo SelectedDeck;

    public static DeckInfo EnemyDeck;

    public static GameMode gameMode;


}
