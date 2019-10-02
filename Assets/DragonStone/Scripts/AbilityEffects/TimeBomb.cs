using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : CreatureEffect
{
    public int buffCooldown = 3;
    public int damage = 4;
    public TimeBomb(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                DealDamageEffect(target, damage);
                AddBuff(target, "Bomb", buffCooldown);                
            }
            
            base.UseEffect();
        }
    }
}