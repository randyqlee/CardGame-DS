using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class SealMagic : CreatureEffect 

{
    public int buffCooldown = 1;
    public int changeCD = 1;
    public SealMagic(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_AfterAttacking += UseEffect;        
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_AfterAttacking += UseEffect;            
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            ShowAbility();
            foreach (CreatureLogic cl in owner.EnemyList())
            {
                if(cl == target)
                {
                    foreach (CreatureEffect ce in cl.creatureEffects)
                    {
                        ce.remainingCooldown = ce.creatureEffectCooldown;
                        ce.UpdateCooldown();
                    }
                }
                else
                {
                    foreach (CreatureEffect ce in cl.creatureEffects)
                    {
                        ce.remainingCooldown += changeCD;
                        ce.UpdateCooldown();
                    }

                }
            }              
    
            
            base.UseEffect();
        }
    }
}