﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleCrush : CreatureEffect
{

    public int buffCooldown = 1;
    public int multiplier = 3;
    
    public TripleCrush(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            //Attack inflicts Brand on target enemy and deals 3 times your damage this turn.
            ShowAbility();
            
            AddBuff(target, "Brand", buffCooldown);
            
            int damage = multiplier * creature.AttackDamage;
            new DealDamageCommand(target.ID, damage, healthAfter: target.TakeOtherDamageVisual(creature.DealOtherDamage(damage)), armorAfter: target.TakeArmorDamageVisual(creature.DealOtherDamage(damage))).AddToQueue();
            target.TakeOtherDamage(damage);

            base.UseEffect();
        }
    }

}
