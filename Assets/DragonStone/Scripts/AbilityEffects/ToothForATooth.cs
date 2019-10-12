using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothForATooth : CreatureEffect
{
    public int buffCooldown = 1;

    public ToothForATooth(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
        foreach (CreatureLogic cl in owner.AllyList())
        {
            if (cl != creature)
            {
                cl.e_IsAttacked += UseEffect;
            }

        }
  
    }

    public override void UnRegisterEventEffect()
    {
        foreach (CreatureLogic cl in owner.AllyList())
        {
            if (cl != creature)
            {
                cl.e_IsAttacked -= UseEffect;
            }

        }      
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (ChanceOK(creature.chance))
        {
            if (target.owner == owner)
            {
                ShowAbility();
                AddBuff(creature, "Lucky",buffCooldown);
            }
            base.UseEffect();
        }

    }
}
