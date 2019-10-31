using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSpeedAhead : CreatureEffect
{
    public int buffCooldown = 2;

    // Start is called before the first frame update
    public FullSpeedAhead(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            
            foreach(CreatureLogic cl in owner.AllyList())
            {
                ShowAbility();
                if(cl != creature)
                {
                    foreach(CreatureEffect ce in cl.creatureEffects)
                    {
                        ce.SkillReduceCreatureEffectCooldown();
                    }
                    AddBuff(cl, "Lucky", buffCooldown);
                }
            }


            base.UseEffect();
        }
    }
}
