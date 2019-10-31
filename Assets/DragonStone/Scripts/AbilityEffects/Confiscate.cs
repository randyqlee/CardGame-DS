using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confiscate : CreatureEffect
{
    public int buffCooldown = 2;
    public Confiscate(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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

        if (CanUseAbility())
        {
            ShowAbility();
            
            foreach (BuffEffect be in target.buffEffects)
            {
                if (be.isBuff)
                {
                    AddBuff(creature, be.Name, buffCooldown);
                }
            }

            if (target.buffEffects != null)
            {
                target.RemoveBuffsAll();
            }

            AddBuff(creature, "Recovery", buffCooldown);

            AddBuff(target, "AntiBuff", buffCooldown);

            base.UseEffect();
        }

    }
}
