using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerOfProtection : CreatureEffect
{
    public int buffCooldown = 1;

    public PrayerOfProtection(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (CanUseAbility())
        {

            ShowAbility();
            RemoveDeBuffsAll(creature);
            AddBuff(creature, "Invincibility", buffCooldown);
            base.UseEffect();
        }
    }
}