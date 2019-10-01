using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandOfHell : CreatureEffect
{
    public int buffCooldown = 2;

    bool effectChance = false;

    public BrandOfHell(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {  
       creature.e_PreAttackEvent += UseEffect;
       creature.e_AfterAttacking += RemoveEffect;  
      
    }

    public override void UnRegisterEventEffect()
    {
       creature.e_PreAttackEvent -= UseEffect; 
       creature.e_AfterAttacking += RemoveEffect;
        
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {
            ShowAbility();
            AddBuff(target, "Brand", buffCooldown);
            AddBuff(target, "CrippledStrike", buffCooldown);

            creature.CriticalChance += 1;

            effectChance = true;
        }
    }

    public void RemoveEffect(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {
            if(effectChance)
            {  
                creature.CriticalChance -= 1;
            }
            base.UseEffect();
        }
    }



}
