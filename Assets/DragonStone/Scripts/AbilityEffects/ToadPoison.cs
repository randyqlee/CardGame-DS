using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadPoison : CreatureEffect
{
    public int buffCooldown = 2;
    public ToadPoison(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                RemoveRandomBuff(target);
                
                AddBuff(target, "Poison", buffCooldown);
                AddBuff(target, "AntiBuff", buffCooldown);
            }


            base.UseEffect();

        }
    }
}
