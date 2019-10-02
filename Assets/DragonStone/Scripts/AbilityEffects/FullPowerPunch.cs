using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPowerPunch : CreatureEffect
{
    public int buffCooldown = 2;

    public FullPowerPunch(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if(CanUseAbility())
        {
            
            DealDamageEffect(target, creature.Health);
            AddBuff(creature, "Stun", buffCooldown);
            base.UseEffect();
        }
    }
}