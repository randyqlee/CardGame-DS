using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRecovery : CreatureEffect
{

    public DarkRecovery(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        //if(creatureEffectCooldown <= 0 && !hasUsedEffect)
        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();

            target.e_ThisCreatureDies += GainExtraTurn;
            int damage = target.Health / 2;
            new DealDamageCommand(target.ID, damage, healthAfter: target.TakeOtherDamageVisual(damage), armorAfter:target.TakeArmorDamageVisual(damage)).AddToQueue();
            target.TakeOtherDamage (damage);

            creature.Heal(damage);
            target.e_ThisCreatureDies -= GainExtraTurn;

            base.UseEffect();


        }
    }

    public void GainExtraTurn(CreatureLogic target)
    {
        creature.AttacksLeftThisTurn++;        
    }
}
