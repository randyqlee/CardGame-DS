﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringWings : CreatureEffect
{
    public int buffCooldown = 2;

    public int stunCooldown = 1;

    bool critFlag = false;

    // Start is called before the first frame update
    public SoaringWings(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += PreAttack;  
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= PreAttack; 
       creature.e_AfterAttacking -= UseEffect;           
    }

    public void PreAttack (CreatureLogic target)
    {
        if (CanUseAbility())
        {
            if(creature.isPrimaryForm)
            {
                ShowAbility();
                creature.CriticalChance += 1;
                critFlag = true;
            }
        }

    }

    public override void UseEffect(CreatureLogic target)
    {
        

        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();
            
            if(creature.isPrimaryForm)
            {
                creature.isPrimaryForm = false;
                AddBuff(creature, "Armor", buffCooldown);
                creature.ExtraTurn();
                if(critFlag)
                {
                    creature.CriticalChance -= 1;
                    critFlag = false;
                }
            }

            else
            {
                AddBuff(target, "Stun", stunCooldown);

                creature.isPrimaryForm = true;
                AddBuff(creature, "IncreaseAttack", buffCooldown);
                creature.ExtraTurn();
                
            }

            base.UseEffect();
        }


    }


}