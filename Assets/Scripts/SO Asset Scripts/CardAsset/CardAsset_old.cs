﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetingOptions
{
    NoTarget,
    AllCreatures, 
    EnemyCreatures,
    YourCreatures, 
    AllCharacters, 
    EnemyCharacters,
    YourCharacters
}
/*
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
*/
public class CardAsset_old : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("General info")]
    public CharacterAsset characterAsset;  // if this is null, it`s a neutral card
    [TextArea(2,3)]
    public string Description;  // Description for spell or character
	public Sprite CardImage;
    public int ManaCost;
    public Sprite HeroPortrait;

    public int cardCost = 10;

    [Header("Creature Info")]
    public int MaxHealth;   // =0 => spell card
    public int Attack;
    public int Armor = 0;
    public int AttacksForOneTurn = 1;
    public bool Charge;
    //DS
    public int Chance;
    public string CreatureScriptName;
    public int specialCreatureAmount;



    //DS
    [Header("Abilities")]
    public List<AbilityAsset> Abilities;

    public AbilityAsset equipAbility;

    //public List<AbilityEffect> abilityEffect; 
    //DS

    [Header("SpellInfo")]
    public string SpellScriptName;
    public int specialSpellAmount;
    public TargetingOptions Targets;

    public RarityOptions Rarity;
    public CreatureType creatureType;
    public CreatureElement creatureElement;

    public TypesOfCards TypeOfCard;

}
