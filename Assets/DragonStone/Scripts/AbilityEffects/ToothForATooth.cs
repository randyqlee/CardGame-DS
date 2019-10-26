using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothForATooth : CreatureEffect
{
    public int buffCooldown = 1;
    List<CreatureLogic> heroAllies = new List<CreatureLogic>();

    public ToothForATooth(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        heroAllies = owner.AllyList();
    }

   public override void RegisterEventEffect()
    {
        foreach (CreatureLogic cl in heroAllies)
        {
            if (cl != creature)
            {
                cl.e_IsAttacked += UseEffect;
            }

        }
  
    }

    public override void UnRegisterEventEffect()
    {
        foreach (CreatureLogic cl in heroAllies)
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
                AddBuff(target, "Lucky",buffCooldown);
            }
            base.UseEffect();
        }

    }
}
