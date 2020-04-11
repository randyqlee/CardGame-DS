using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrathfulAttack : CreatureEffect
{
    public int buffCooldown = 2;

    bool effectChance = false;
    public WrathfulAttack(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            
            if(ChanceOK(creature.chance))
            {
                //ShowAbility();
                
                effectChance = true;
                creature.CriticalChance += 1;

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
                AddBuff(target, "Brand", buffCooldown);
                creature.CriticalChance -= 1;
            }

            base.UseEffect();
        }
    }

}
