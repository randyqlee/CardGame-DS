using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : CreatureEffect
{
    public int buffCooldown = 3;

    public ThunderStrike(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (CanUseAbility())
        {
            ShowAbility();
            AddBuff(creature, "Armor", buffCooldown); 
            AddBuff(creature, "Shield", buffCooldown); 
            AddBuff(target, "Bomb", buffCooldown);    
            base.UseEffect();

        }
    }
}
