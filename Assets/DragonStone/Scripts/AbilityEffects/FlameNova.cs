using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameNova : CreatureEffect
{
    public int criticalLife = 15;
    bool criticalActivated = false;

    public FlameNova(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            ShowAbility();
            if(target.Health > criticalLife)
            {
                creature.CriticalChance += 1;
                criticalActivated = true;
            }
        }

    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            if (criticalActivated)
                creature.CriticalChance -= 1;
            else
                target.Health = 0;

            base.UseEffect();

        }
    }
}