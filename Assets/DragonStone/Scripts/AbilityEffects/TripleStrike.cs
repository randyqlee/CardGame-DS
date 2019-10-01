using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleStrike : CreatureEffect
{
    public int buffCooldown = 2;

    public TripleStrike(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (creatureEffectCooldown <= 0)
        {
            ShowAbility();
            AddBuff(target, "Brand", buffCooldown);
            AddBuff(target, "DecreaseAttack", buffCooldown);
            AddBuff(target, "Unhealable", buffCooldown);
            base.UseEffect();
        }
    }
}
