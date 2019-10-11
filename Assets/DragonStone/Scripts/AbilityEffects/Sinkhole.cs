using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinkhole : CreatureEffect
{
   public int increaseCD = 1;

    public Sinkhole(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            int totalDamage = 0;
            
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                if (cl != target)
                {
                    DealDamageEffect(cl, creature.AttackDamage);
                    totalDamage += creature.AttackDamage;
                }
                
                foreach (CreatureEffect ce in cl.creatureEffects)
                {
                    ce.remainingCooldown += increaseCD;
                    ce.UpdateCooldown();
                }
                creature.Heal(totalDamage);

            }  
            base.UseEffect();
        }

    }    


}
