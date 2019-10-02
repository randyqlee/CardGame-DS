using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingBlood : CreatureEffect
{
    public int buffCooldown = 1;

    public BoilingBlood(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_AfterAttacking += UseEffect;        
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_AfterAttacking += UseEffect;            
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            ShowAbility();
            creature.Heal(creature.AttackDamage);

            if(ChanceOK(creature.chance))
            {
                AddBuff(target, "Berserk", buffCooldown);
            }
            
            base.UseEffect();
        }
    }
}