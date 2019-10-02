using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryOfProvocation : CreatureEffect
{
    public int buffCooldown = 2;

    public CryOfProvocation(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                AddBuff(creature, "Taunt", buffCooldown);
                AddBuff(creature, "Armor", buffCooldown);

            }
            
            base.UseEffect();
        }
    }
}