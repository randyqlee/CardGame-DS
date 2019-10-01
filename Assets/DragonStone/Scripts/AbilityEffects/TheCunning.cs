using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCunning : CreatureEffect
{
    public int damage = 5;
    public TheCunning(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (creatureEffectCooldown <= 0)
        {
            ShowAbility();
            if(Random.Range(0,100)<=creature.chance)
            {
                List<BuffEffect> buffList = new List<BuffEffect>();
                if (target.buffEffects != null)
                {
                    foreach(BuffEffect be in target.buffEffects)
                    {
                        if (be.isBuff)
                        {
                            buffList.Add(be);
                        }
                    }
                }
                if (buffList != null)
                {
                    int i = Random.Range(0,buffList.Count);
                    AddBuff(creature, buffList[i].Name, buffList[i].buffCooldown);
                    target.RemoveBuff(buffList[i]);
                }

            }

            int buffCount = 0;
            foreach (BuffEffect be in creature.buffEffects)
            {
                if (be.isBuff)
                {
                    buffCount++;
                }
            }

            if(buffCount > 0)
            {

                int totalDamage = buffCount * damage;
                if (totalDamage > 0)
                new DealDamageCommand(target.ID, damage, healthAfter: target.TakeOtherDamageVisual(totalDamage), armorAfter: target.TakeArmorDamageVisual(totalDamage)).AddToQueue();
                target.TakeOtherDamage(damage);
            }

            base.UseEffect();
        }
    }

}
