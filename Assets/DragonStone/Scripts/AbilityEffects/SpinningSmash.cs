using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSmash : CreatureEffect
{
    public int buffCooldown = 1;
    bool effectChance = false;
    bool armorOn = false;
    public SpinningSmash(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += CheckChance;      
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= CheckChance;  
        creature.e_AfterAttacking -= UseEffect;          
    }

    public void CheckChance(CreatureLogic target)
    {
        if(creatureEffectCooldown <= 0)
        {
            effectChance = false;
            
            if(Random.Range(0,100)<=creature.chance)
            {
                ShowAbility();
                
                effectChance = true;
                if(creature.Armor > 0)
                {
                    armorOn = true;
                    creature.CriticalChance += 1;
                }

            }
        }

    }
    public override void UseEffect(CreatureLogic target)
    {
        if(creatureEffectCooldown <= 0)
        {

            if(effectChance)
            { 

                creature.SplashAttackDamage(target,creature.AttackDamage);
                foreach(CreatureLogic cl in owner.EnemyList())
                {
                    AddBuff(cl, "CrippledStrike", buffCooldown);
                }  

                if (armorOn)         
                    creature.CriticalChance -= 1;
            }

            base.UseEffect();
        }
    }
}
