using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSurge : CreatureEffect
{
    public int buffCooldown = 1;
    public MagicSurge(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {  
    
        creature.e_PreAttackEvent += PreAttack;  
        
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= PreAttack;
       creature.e_AfterAttacking -= UseEffect;          
    }

    public void PreAttack (CreatureLogic target)
    {
        if (CanUseAbility())
        {
            //ShowAbility();
            creature.CriticalChance += 1;
        }

    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            ShowAbility();

            creature.SplashAttackDamage(target, creature.AttackDamage);
            
            foreach(CreatureLogic cl in owner.EnemyList())
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
                        BuffSystem.Instance.RemoveBuff(cl,be);
                        buffRemoved = true;
                    }
                }

                if(!buffRemoved)
                {
                    AddBuff (cl, "Silence", buffCooldown);
                }

            }

            creature.CriticalChance -= 1;
            base.UseEffect();

        }
    }
}