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
        foreach(CreatureLogic cl in owner.EnemyList())
            cl.e_buffApplied += BuffApplied;      
    }

    public override void UnRegisterEventEffect()
    {

        foreach(CreatureLogic cl in owner.EnemyList())
            cl.e_buffApplied -= BuffApplied;         
    }

    public void BuffApplied(CreatureLogic target, BuffEffect be)
    {
        if (CanUseAbility())
        {
            if (target.owner != owner && be.isBuff)
            {
                if(ChanceOK(creature.chance))
                {
                    ShowAbility();
                    AddBuff (creature, "Armor", buffCooldown);
                }
                
            }
            base.UseEffect();
        }
    }



}
