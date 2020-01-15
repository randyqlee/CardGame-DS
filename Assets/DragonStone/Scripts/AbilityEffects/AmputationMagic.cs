using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmputationMagic : CreatureEffect
{
    bool effectChance = false;
    int buffCooldown = 1;

    public AmputationMagic(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (CanUseAbility())
        {
            effectChance = false;
            
            if(Random.Range(0,100)<=creature.chance)
            {
                //ShowAbility();
                
                effectChance = true;
                //creature.CriticalChance += 1;

            }
        }

    }
    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {

            if(effectChance)
            { 
                ShowAbility();
                creature.SplashAttackDamage(target,creature.AttackDamage);

                //creature.CriticalChance -= 1;

                AddBuff(target, "Poison", buffCooldown);
            }

            base.UseEffect();
        }
    }

}

