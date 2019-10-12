using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalError : CreatureEffect
{
    public int buffCooldown = 1;
    public CriticalError(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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

            creature.SplashAttackDamage(target,creature.AttackDamage);
            
            foreach(CreatureLogic cl in owner.EnemyList())
            {
                if (!cl.isDead)
                {
                    var buffList = new List<BuffEffect>();

                    foreach (BuffEffect be in cl.buffEffects)
                    {
                        if(be.isBuff)
                        {
                            buffList.Add(be);
                        }
                    }

                    bool buffRemoved = false;

                    if (buffList != null)
                    {
                        foreach(BuffEffect be in buffList)
                        {
                            cl.RemoveBuff(be);
                            buffRemoved = true;
                        }
                    }


                    if(buffRemoved)
                    {
                        AddBuff (cl, "Silence", buffCooldown);
                    }
                }

            }

            creature.CriticalChance -= 1;
            base.UseEffect();

        }
    }
}