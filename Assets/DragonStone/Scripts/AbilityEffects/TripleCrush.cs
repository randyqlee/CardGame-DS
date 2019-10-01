using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleCrush : CreatureEffect
{

    public int buffCooldown = 1;
    public TripleCrush(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {    
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {
            //Attack inflicts Brand on target enemy and deals 3 times your damage this turn.
            ShowAbility();
            AddBuff(target, "Brand", buffCooldown);
            new DealDamageCommand(target.ID, creature.AttackDamage, healthAfter: target.TakeOtherDamageVisual(creature.DealOtherDamage(creature.AttackDamage))).AddToQueue();
            target.TakeOtherDamage(creature.AttackDamage);

            base.UseEffect();
        }
    }

}
