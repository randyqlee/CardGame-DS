using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGrudge : CreatureEffect
{
    public int buffCooldown = 1;

    public SmallGrudge(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                AddBuff(creature, "IncreaseAttack", buffCooldown);
                target.RemoveRandomBuff();
            }
            
            base.UseEffect();
        }
    }
}