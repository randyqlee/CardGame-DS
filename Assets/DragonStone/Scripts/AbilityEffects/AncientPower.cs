using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientPower : CreatureEffect
{

    public int buffCooldown = 1;
    public int damage = 7;
    // Start is called before the first frame update
    public AncientPower(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        //Chance to Silence and deal 7 damage to all enemies with debuffs
            if(Random.Range(0,100)<=creature.chance)
            {
                
                foreach (CreatureLogic enemy in owner.EnemyList())
                {
                    foreach (BuffEffect be in enemy.buffEffects)
                    {
                        if (be.isDebuff)
                        {
                            ShowAbility();
                            AddBuff(enemy, "Silence", buffCooldown);
                            new DealDamageCommand(enemy.ID, damage, healthAfter: enemy.TakeOtherDamageVisual(enemy.DealOtherDamage(damage))).AddToQueue();
                            enemy.TakeOtherDamage(damage);
                            break;
                        }
                    }
                }
                
            }
            base.UseEffect();
        }
    }


}
