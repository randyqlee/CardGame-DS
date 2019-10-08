﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditate : CreatureEffect
{
    public int maxNumberOfAllies = 2;

    public Meditate(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_PreAttackEvent += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {            
            List<CreatureLogic> sortedList = owner.SortAllyListByHealth();
            if (sortedList.Count > 1)
            {
                ShowAbility();
                int count = 0;
                for (int i = 0; i<sortedList.Count && count < maxNumberOfAllies; i++)
                {
                    if (sortedList[i] != creature)
                    {
                        sortedList[i].Heal(creature.Health);
                        count++;
                    }
                }
            }
            base.UseEffect();
        }
    }
}