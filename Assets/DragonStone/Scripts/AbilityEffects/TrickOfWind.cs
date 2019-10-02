using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickOfWind : CreatureEffect
{
    public int buffCooldown = 2;
    public int damageMult = 3;
    public TrickOfWind(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (CanUseAbility() && ChanceOK(creature.chance))
        {
            ShowAbility();

            AddBuff(target, "Brand",buffCooldown);
            int damage = damageMult * (creature.Health / 10);
            DealDamageEffect(target, damage);

            base.UseEffect();

        }
    }
}
