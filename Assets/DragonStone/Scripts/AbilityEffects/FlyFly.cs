using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFly : CreatureEffect
{
    public int buffCooldown = 1;
    public FlyFly(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {  
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
       creature.e_AfterAttacking -= UseEffect;          
    }


    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            ShowAbility();

            creature.SplashAttackDamage(target, creature.AttackDamage);
            
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                
                AddBuff (cl, "CrippledStrike", buffCooldown);

            }
            base.UseEffect();

        }
    }
}