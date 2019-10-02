using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class DelayedPromise : CreatureEffect
{

    public int buffCooldown = 1;

    public DelayedPromise(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {
            ShowAbility();
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                                
                AddBuff(cl, "Silence", buffCooldown);
                
            }
            AddBuff(target, "Stun", buffCooldown);
                     
            base.UseEffect();
        }
    }
}