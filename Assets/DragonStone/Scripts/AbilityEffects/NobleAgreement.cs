using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobleAgreement : CreatureEffect
{

    public NobleAgreement(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }
    public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += IncreaseAttackCount;
       creature.e_SecondAttack += UseEffect;       
 
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= IncreaseAttackCount;
        creature.e_SecondAttack -= UseEffect;   
         
    }

    void IncreaseAttackCount(CreatureLogic target)
    {
       
        if(CanUseAbility())
        {
            ShowAbility();
            creature.AttacksLeftThisTurn++;
            
            creature.CriticalChance += 1;   
            
        }                             
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            ShowAbility();
            if(!target.isDead)          
                creature.AttackCreature(target);

            creature.CriticalChance -= 1;
            base.UseEffect();  

        }

    }
}
