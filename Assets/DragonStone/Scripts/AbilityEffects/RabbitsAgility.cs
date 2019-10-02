using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitsAgility : CreatureEffect
{
   public int buffCooldown = 1;

    public RabbitsAgility(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            
            if(Random.Range(0,100)<=creature.chance)
            {
                ShowAbility();
                creature.RemoveDeBuffsAll();
                AddBuff(creature, "Immunity", buffCooldown);

            }

            base.UseEffect();
        }

    }    


}
