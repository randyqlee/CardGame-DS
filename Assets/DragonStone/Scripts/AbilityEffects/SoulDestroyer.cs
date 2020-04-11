using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDestroyer : CreatureEffect
{
    public int buffCooldown = 4;

    public SoulDestroyer(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       foreach(CreatureLogic cl in owner.EnemyList())
            cl.e_ThisCreatureDies += UseEffect;       
 
    }

    public override void UnRegisterEventEffect()
    {
       foreach(CreatureLogic cl in owner.EnemyList())
            cl.e_ThisCreatureDies -= UseEffect;      
         
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {

            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                target.hasCurse = true;

                AddBuff(creature, "Resurrect", buffCooldown);
                
            }

            base.UseEffect();
            
        }
    }
            

}
