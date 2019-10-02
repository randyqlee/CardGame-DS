using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : CreatureEffect
{
    public int buffCooldown = 1;
    public int damage = 7;

    public Capture(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            AddBuff(target, "Stun", buffCooldown);

            for (int i = target.buffEffects.Count - 1; i>=0; i--)
            {
                if (target.buffEffects[i].isBuff)
                {
                    target.RemoveBuff(target.buffEffects[i]);
                    DealDamageEffect(target, damage);
                }
            }

            base.UseEffect();
        }
    }


}
