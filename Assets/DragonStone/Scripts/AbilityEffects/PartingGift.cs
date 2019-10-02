using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartingGift : CreatureEffect
{
    public int buffCooldown = 1;
    public int increaseCD = 2;

    public PartingGift(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            ShowAbility();
            AddBuff(creature, "LifeSteal",buffCooldown);
            AddBuff(target, "Stun",buffCooldown);

            foreach (CreatureEffect ce in target.creatureEffects)
            {
                ce.creatureEffectCooldown += increaseCD;
            }
         
            base.UseEffect();

        }
    }
}
