using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterEgoAttack : CreatureEffect
{
    public int buffCooldown = 1;
    public int buffCooldown2 = 2;

    public AlterEgoAttack(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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

            AddBuff(creature, "Invincibility",buffCooldown);
            AddBuff(creature, "Immunity",buffCooldown2);
            
            target.RemoveRandomBuff();

            base.UseEffect();

        }
    }
}
