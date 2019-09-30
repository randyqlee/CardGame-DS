using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBlow : CreatureEffect
{
    public int buffCooldown = 1;

    bool effectChance = false;

    public WildBlow(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (creatureEffectCooldown <= 0)
        {
            effectChance = false;
            if(Random.Range(0,100)<=creature.chance)
            {
                ShowAbility();                
                effectChance = true;

                if(creature.isPrimaryForm)
                {
                    AddBuff(creature, "LifeSteal", buffCooldown);
                    creature.CriticalChance += 1;
                }
                else
                {
                    AddBuff(creature, "LifeSteal", buffCooldown);
                    AddBuff(creature, "Taunt", buffCooldown);
                }
            }
        }

    }
    public override void UseEffect(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {
            if(effectChance)
            {  
                if(creature.isPrimaryForm)
                {            
                    creature.CriticalChance -= 1;
                }
            }
            base.UseEffect();
        }
    }
}
