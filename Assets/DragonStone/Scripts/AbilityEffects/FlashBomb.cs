using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBomb : CreatureEffect
{
    public int buffCooldown = 1;

    public FlashBomb(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                                
                AddBuff(cl, "CrippledStrike", buffCooldown);
                AddBuff(cl, "Berserk", buffCooldown);
            }
                     
            base.UseEffect();
        }
    }
}