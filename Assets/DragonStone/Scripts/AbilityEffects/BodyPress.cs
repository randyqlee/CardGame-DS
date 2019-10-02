using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPress : CreatureEffect
{
    public int buffCooldown = 2;

    public BodyPress(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                AddBuff(creature, "Armor", buffCooldown);
                AddBuff(creature, "Taunt", buffCooldown);
            }
            
            base.UseEffect();
        }
    }
}