using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessAssault : CreatureEffect
{

    public RecklessAssault(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            int damage = creature.Health;
            int heal = creature.Health/2;
            DealDamageEffect(target, damage);
            creature.Heal(0-heal);
            
            base.UseEffect();
        }
    }
}