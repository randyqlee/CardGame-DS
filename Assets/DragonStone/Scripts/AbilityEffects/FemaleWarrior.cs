using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleWarrior : CreatureEffect
{
    public int buffCooldown = 2;

    public int damage = 5;

    public FemaleWarrior(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(Random.Range(0,100)<=creature.chance)
            {
                ShowAbility();
                foreach(CreatureLogic cl in owner.AllyList())
                {
                    AddBuff(cl, "Defense", buffCooldown);
                }

                foreach(CreatureLogic cl in owner.EnemyList())
                {
                    foreach(BuffEffect be in cl.buffEffects)
                    {
                        if(be.isBuff)
                        {
                            new DealDamageCommand(cl.ID, damage, healthAfter: cl.TakeOtherDamageVisual(damage), armorAfter:cl.TakeArmorDamageVisual(damage)).AddToQueue();
                            cl.TakeOtherDamage(damage);
                            break;
                        }
                    }
                }
            }
            base.UseEffect();
        }
    }

}

