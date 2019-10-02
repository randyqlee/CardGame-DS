using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedShots : CreatureEffect
{
    public int buffCooldown = 3;

    public FocusedShots(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {  
    
        creature.e_PreAttackEvent -= PreAttack;  
        
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
            target.RemoveBuffsAll();

            AddBuff(target, "Bomb",buffCooldown);

            creature.CriticalChance -= 1;
            base.UseEffect();

        }
    }
}