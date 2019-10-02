using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainsPower : CreatureEffect
{
    public int heal = 6;
    public int damage = 3;
    public MountainsPower(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                foreach(CreatureLogic cl in owner.AllyList())
                {
                    cl.Heal(heal);
                }
                DealDamageEffect(target, damage);
            }


            base.UseEffect();

        }
    }
}
