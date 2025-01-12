﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class BestPartner : CreatureEffect 
{
    
   

    public BestPartner(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();

                if (owner.AllyList().Count > 1)
                {
                    CreatureLogic ally = owner.GetRandomAlly(creature);
                    
                    ally.AttacksLeftThisTurn++;
                    ally.PreAttack(target);

                     
                    //CreatureLogic.CreaturesCreatedThisGame[ally.ID].AttackCreatureWithID(target.ID);
                }


            }
            
            base.UseEffect();
        }
    }

}