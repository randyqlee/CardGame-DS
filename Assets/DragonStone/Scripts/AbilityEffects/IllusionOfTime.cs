using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionOfTime : CreatureEffect
{
    public int buffCooldown = 1;

    public IllusionOfTime(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
       //cant refresh this cooldown
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
            foreach(CreatureLogic cl in owner.AllyList())
            {
                cl.RemoveDeBuffsAll();
                foreach(CreatureEffect ce in cl.creatureEffects)
                {
                    ce.SkillRefreshCreatureEffectCooldown();
                }


                AddBuff(cl, "Lucky",buffCooldown);
            }

            base.UseEffect();

        }
    }
}