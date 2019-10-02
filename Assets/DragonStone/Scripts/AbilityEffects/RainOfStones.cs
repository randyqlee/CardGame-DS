using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfStones : CreatureEffect
{
    public int buffCooldown = 1;

    public RainOfStones(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            ShowAbility();
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                DealDamageEffect(cl, creature.AttackDamage);
                foreach (CreatureEffect ce in cl.creatureEffects)
                {
                    AddBuff (cl, "Silence", buffCooldown);
                    ce.remainingCooldown = ce.creatureEffectCooldown;
                    ce.UpdateCooldown();
                }

            }  
            base.UseEffect();
        }

    }
}