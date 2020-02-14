using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DutyOfTheMonarch : CreatureEffect
{
    public int changeCD = 1;
    public DutyOfTheMonarch(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();

                foreach(CreatureLogic cl in owner.AllyList())
                {
                    foreach(CreatureEffect ce in cl.creatureEffects)
                    {
                        ce.remainingCooldown -= changeCD;
                       // ce.UpdateCooldown();
                    }
                    foreach(BuffEffect be in cl.buffEffects)
                    {
                        if (be.isBuff)
                            be.buffCooldown += changeCD;
                            //be.UpdateCooldown();
                    }

                }
                
            }
            
            base.UseEffect();
        }
    }
}