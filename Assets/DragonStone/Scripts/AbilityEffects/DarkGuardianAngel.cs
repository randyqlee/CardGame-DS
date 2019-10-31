using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkGuardianAngel : CreatureEffect
{
    public int buffCooldown = 2;

    // Start is called before the first frame update
    public DarkGuardianAngel(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            //AddBuff(target, "Unhealable", buffCooldown);
            
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                    AddBuff(cl, "Unhealable", buffCooldown);
            }

            
            
            foreach(CreatureLogic cl in owner.AllyList())
            {
                    AddBuff(cl, "IncreaseAttack", buffCooldown);
            }


            base.UseEffect();
        }
    }



}
