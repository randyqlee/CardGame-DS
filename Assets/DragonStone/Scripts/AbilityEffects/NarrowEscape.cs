using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrowEscape : CreatureEffect
{
    public int maxNumberOfAllies = 1;
    public int healValue = 8;

    public NarrowEscape(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
    }

    public override void UnRegisterEventEffect()
    {
     
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            ShowAbility();
 

            base.UseEffect();
        }
    }

}