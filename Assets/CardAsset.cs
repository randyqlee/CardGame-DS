using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RarityOptions
{
    Basic,
    Rare,
    Epic,
    Legendary,
    Common


}

public enum CreatureType
{
    Attack,
    HP,
    Support


}

public enum CreatureElement
{
    White,
    Green,
    Blue,
    Red,
    Black


}

public enum TypesOfCards
{
    Creature,
    Spell


}

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("General info")]

    [TextArea(2,3)]
    public string cardName;
    public string Description;  // Description for spell or character
	public Sprite CardImage;

    public Sprite CardImage_Secondary;
    public Sprite HeroPortrait;

    public int cardCost = 100;

    public RarityOptions Rarity;
    public CreatureType creatureType;
    public CreatureElement creatureElement;

    [Header("Creature Info")]
    public int MaxHealth;

    public int Attack;
    public int Armor = 0;
    public int AttacksForOneTurn = 1;
    //DS
    public int Chance;


    //DS
    [Header("Abilities")]
    public List<AbilityAsset> Abilities;

    public AbilityAsset equipAbility;

    public TypesOfCards TypeOfCard = TypesOfCards.Creature;
    public CharacterAsset characterAsset; 
    public string CreatureScriptName;
    public int specialCreatureAmount;
    public string SpellScriptName;
    public int specialSpellAmount;
    public TargetingOptions Targets;
    public int ManaCost;

}
