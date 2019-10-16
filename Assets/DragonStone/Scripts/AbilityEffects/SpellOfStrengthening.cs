using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOfStrengthening : CreatureEffect
{
    public int buffCooldown = 2;

    public SpellOfStrengthening(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_PreAttackEvent += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {
            ShowAbility();

            foreach (CreatureLogic creature in owner.AllyList())
            {

                AddBuff(creature, "Immunity",buffCooldown);
                AddBuff(creature, "IncreaseAttack",buffCooldown);
                AddBuff(creature, "Defense",buffCooldown);
            }
            
            base.UseEffect();

        }
    }
}
