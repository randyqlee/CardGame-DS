using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitsAgility : CreatureEffect
{
   public int buffCooldown = 1;

    public RabbitsAgility(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        this.cooldownCantChange = true;
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
            ShowAbility();
            CreatureLogic cl = owner.GetRandomAlly(creature);
            if (cl != null)
            {
                foreach(CreatureEffect ce in cl.creatureEffects)
                {
                    ce.SkillRefreshCreatureEffectCooldown();
                }
                AddBuff(cl, "Evasion",buffCooldown);
            }

            base.UseEffect();

        }

    }    


}
