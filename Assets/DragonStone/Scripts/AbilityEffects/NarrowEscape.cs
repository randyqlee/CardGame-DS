using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrowEscape : CreatureEffect
{
 
    int tempDamageReduction;

    public NarrowEscape(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        tempDamageReduction = creature.DamageReduction;
    }

   public override void RegisterEventEffect()
    {
       
        creature.e_PreDealDamage += UseEffect;
        creature.e_IsAttacked += DamageReductionReturn;

    }

    public override void UnRegisterEventEffect()
    {
           creature.e_PreDealDamage -= UseEffect;
           creature.e_IsAttacked -= DamageReductionReturn;
    }

    //
    public override void UseEffect(CreatureLogic attacker)
    {
        if(CanUseAbility())
        {               
           if(ChanceOK(creature.chance))
            {
               

                int totalLife = creature.Health + creature.Armor;
                  

                if(attacker.AttackDamage >= totalLife)
                {
                    ShowAbility();
                    creature.DamageReduction = 0;
                    Debug.Log("Damage Reduction Value Pre-Attack: " +creature.DamageReduction);
                }
                
            }
 

            base.UseEffect();
        }
    }//Use Effect

    public void DamageReductionReturn(CreatureLogic target)
    {
        creature.DamageReduction = tempDamageReduction;
        Debug.Log("Damage Reduction Value Post Attack: " +creature.DamageReduction);
    }

}