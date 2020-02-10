using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : CreatureEffect
{
    public int buffCooldown = 1;

    public DragonAttack(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            ShowAbility();
            creature.CriticalChance += 1;
        }

    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            RemoveBuffsAll(target);

            AddBuff(target, "Stun",buffCooldown);

            creature.CriticalChance -= 1;
            base.UseEffect();

        }
    }
}