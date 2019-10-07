
using UnityEngine;
using System;
using System.Collections.Generic;
using SimpleJSON;
using CastleDBImporter;
namespace CompiledTypes
{ 
    public class Creatures
    {
        public string id;
public string cardName;
public creatureTypeEnum creatureType;
public enum creatureTypeEnum {  Attack = 0,HP = 1,Support = 2 }public creatureElementEnum creatureElement;
public enum creatureElementEnum {  White = 0,Green = 1,Blue = 2,Red = 3,Black = 4 }public int Chance;
public List<Abilities> AbilitiesList = new List<Abilities>();
public int MaxHealth;
public int Attack;
public string description;
public string CardImage;
public string HeroPortrait;
public int cardCost;
public RarityEnum Rarity;
public enum RarityEnum {  Common = 0,Rare = 1,Epic = 2,Legendary = 3 }public int Armor;
public int AttacksForOneTurn;
public AbilityEffects equipAbility;

        public enum RowValues { 
Giana, 
Ragdoll, 
MoLong, 
Hathor, 
Ganymede, 
YeonHong, 
Artamiel, 
Shaina, 
Tesarion, 
Zeratu, 
Pater_Druid, 
Pater_Beast, 
Vivachel, 
Betta, 
Sylvia, 
Nicki, 
Frigate, 
Icares, 
Avaris, 
Aegir, 
Sabrina, 
Chilling, 
Bolverk, 
ShiHou, 
Megan, 
Woosa, 
Izaria, 
Ritesh, 
Christina, 
Jamire, 
Ethna, 
Lushen, 
Copper, 
Tiana, 
Diana_Unicorn, 
Diana_Human, 
Bulldozer, 
Racuni, 
Antares, 
Garo, 
Perna, 
Okeanos, 
Vanessa, 
Verdehile, 
Gemini, 
Tablo, 
Fran, 
Kabilla, 
Wedjat, 
Dover, 
Iris, 
Jeanne
 } 
        public Creatures (CastleDBParser.RootNode root, RowValues line) 
        {
            SimpleJSON.JSONNode node = root.GetSheetWithName("Creatures").Rows[(int)line];
id = node["id"];
cardName = node["cardName"];
creatureType = (creatureTypeEnum)node["creatureType"].AsInt;
creatureElement = (creatureElementEnum)node["creatureElement"].AsInt;
Chance = node["Chance"].AsInt;
foreach(var item in node["Abilities"]) { AbilitiesList.Add(new Abilities(root, item));}
MaxHealth = node["MaxHealth"].AsInt;
Attack = node["Attack"].AsInt;
description = node["description"];
CardImage = node["CardImage"];
HeroPortrait = node["HeroPortrait"];
cardCost = node["cardCost"].AsInt;
Rarity = (RarityEnum)node["Rarity"].AsInt;
Armor = node["Armor"].AsInt;
AttacksForOneTurn = node["AttacksForOneTurn"].AsInt;
equipAbility = new CompiledTypes.AbilityEffects(root,CompiledTypes.AbilityEffects.GetRowValue(node["equipAbility"]));

        }  
        
public static Creatures.RowValues GetRowValue(string name)
{
    var values = (RowValues[])Enum.GetValues(typeof(RowValues));
    for (int i = 0; i < values.Length; i++)
    {
        if(values[i].ToString() == name)
        {
            return values[i];
        }
    }
    return values[0];
}
    }
}