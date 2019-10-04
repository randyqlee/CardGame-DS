using UnityEngine;
using System.Collections.Generic;
using CompiledTypes;
using UnityEditor;

[ExecuteInEditMode]
public class CastleDBTest : MonoBehaviour
{
    public TextAsset CastleDBAsset;
    public bool test;
    void Update()
    {
        if(test)
        {
            CastleDB DB = new CastleDB(CastleDBAsset);
            //Creatures creature = DB.Creatures.Dragon;

           foreach(Creatures creature in DB.Creatures.GetAll())
           {

           //for (int i = 0; i < DB.Creatures.)
           /*
           {
            Debug.Log("[string] name: " + creature.Name);
            Debug.Log("[bool] attacks player: " + creature.attacksPlayer);
            Debug.Log("[int] base damage: " + creature.BaseDamage);
            Debug.Log("[float] damage modifier: " + creature.DamageModifier);
            Debug.Log("[enum] death sound: " + creature.DeathSound);
            Debug.Log("[flag enum] spawn areas: " + creature.SpawnAreas);
            foreach (var item in creature.DropsList)
            {
                Debug.Log($"{creature.Name} drops item {item.item} at rate {item.DropChance}");
                foreach (var effect in item.PossibleEffectsList)
                {
                    Debug.Log($"item has effect {effect.effect} with chase {effect.EffectChance}");
                }
            }
            */

            
            //Creature asset = ScriptableObject.CreateInstance<Creature>();
            CardAsset asset = ScriptableObject.CreateInstance<CardAsset>();
            //asset.creature = creature;

            asset.cardName = creature.cardName;
            asset.Description = creature.description;  // Description for spell or character
            //public Sprite CardImage;
            //public Sprite HeroPortrait;

            asset.cardCost = creature.cardCost;

            //asset.Rarity = creature.Rarity.ToString();
            //asset.creatureType = creature.creatureType;
            //asset.creatureElement = creature.creatureElement;
            asset.MaxHealth = creature.MaxHealth;
            asset.Attack = creature.Attack;
            asset.Armor = creature.Armor;
            asset.AttacksForOneTurn = creature.AttacksForOneTurn;
            asset.Chance = creature.Chance;

            asset.Abilities = new List<AbilityAsset>();
                                    
            foreach(Abilities ae in creature.AbilitiesList)
            {


                AbilityAsset aa = new AbilityAsset();
                
                aa.abilityName = ae.Ability.abilityName;
                aa.description = ae.Ability.description;
                aa.abilityEffect = ae.Ability.abilityEffect;
                aa.abilityCoolDown = ae.Ability.abilityCoolDown;
         
                asset.Abilities.Add(aa);
            }

            asset.equipAbility = new AbilityAsset();

                
            asset.equipAbility.abilityName = creature.equipAbility.abilityName;
            asset.equipAbility.description = creature.equipAbility.description;
            asset.equipAbility.abilityEffect = creature.equipAbility.abilityEffect;
            asset.equipAbility.abilityCoolDown = creature.equipAbility.abilityCoolDown;
            

    
            /*
            asset.Name = creature.Name;
            asset.attacksPlayer= creature.attacksPlayer;
            asset.BaseDamage = creature.BaseDamage;
            asset.DamageModifier = creature.DamageModifier;
            asset.DropsList = creature.DropsList;
            */
            ProjectWindowUtil.CreateAsset(asset, asset.cardName + ".asset");
            //ProjectWindowUtil.CreateAsset(asset, "New" + ".asset");
           }
            
            test = false;
        }
    }
}
