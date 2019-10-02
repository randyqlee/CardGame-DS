using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBreak : CreatureEffect
{
    public int buffCooldown = 2;
    public int damage = 3;
    public ThunderBreak(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                AddBuff(target, "DecreaseAttack",buffCooldown);
                creature.SplashAttackDamage(target, damage);
                
            }


            base.UseEffect();

        }
    }
}
