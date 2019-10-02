using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealOfFire : CreatureEffect
{
   public int maxLife = 15;
   public int buffCooldown = 2;

    public SealOfFire(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if (target.Health > maxLife)
                target.Health = maxLife;
            else
            {
                AddBuff(target, "Bomb",buffCooldown);
                AddBuff(target, "Unhealable",buffCooldown);
            }

              
            base.UseEffect();
        }

    }    


}
