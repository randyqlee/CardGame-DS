using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purify : CreatureEffect
{
    public int buffCooldown = 2;

    public Purify(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                creature.RemoveDeBuffsAll();
                AddBuff(creature, "Recovery", buffCooldown);
                
            }
            
            base.UseEffect();
        }
    }
}