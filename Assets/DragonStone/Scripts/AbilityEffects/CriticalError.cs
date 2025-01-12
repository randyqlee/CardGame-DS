﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalError : CreatureEffect
{
    public int buffCooldown = 1;
    public CriticalError(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {  
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
       creature.e_AfterAttacking -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            ShowAbility();

            creature.SplashAttackDamage(target,creature.AttackDamage);
            
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                if (!cl.isDead)
                {
                    bool buffExists = false;
                    foreach (BuffEffect be in cl.buffEffects)
                    {
                        if(be.isBuff)
                        {
                            buffExists = true;
                            break;
                        }
                    }

                    if (buffExists)
                    {
                        RemoveRandomBuff(cl);
                        AddBuff (cl, "Silence", buffCooldown);

                    }

                }

            }

            creature.CriticalChance -= 1;
            base.UseEffect();

        }
    }
}