using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearOfDevastation : CreatureEffect
{
    public int buffCooldown = 1;

    public SpearOfDevastation(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                BuffEffect rndDebuff = creature.RandomDebuff();
                if(rndDebuff != null)
                {
                    AddBuff(target, rndDebuff.GetType().Name, rndDebuff.buffCooldown);
                    rndDebuff.RemoveBuff();
                }
                BuffEffect rndBuff = target.RandomBuff();
                if(rndBuff != null)
                {
                    AddBuff(creature, rndBuff.GetType().Name, rndBuff.buffCooldown);
                    rndBuff.RemoveBuff();
                }
            }
            
            base.UseEffect();
        }
    }
}