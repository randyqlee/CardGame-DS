using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnKnowledge : CreatureEffect
{
    public int buffCooldown = 2;

    public LearnKnowledge(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
    
       creature.e_buffApplied += BuffApplied;      
    }

    public override void UnRegisterEventEffect()
    {

        creature.e_buffApplied -= BuffApplied;          
    }

    public void BuffApplied(CreatureLogic target, BuffEffect be)
    {
        if(creatureEffectCooldown <= 0)
        {
            if (target.owner != owner && be.isBuff)
            {
                if(Random.Range(0,100)<=creature.chance)
                {
                    ShowAbility();
                    AddBuff (creature, "Armor", buffCooldown);
                }
                
            }
            base.UseEffect();
        }
    }



}
