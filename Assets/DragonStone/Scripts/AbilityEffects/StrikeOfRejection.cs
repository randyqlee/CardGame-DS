using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeOfRejection : CreatureEffect
{
    public int buffCooldown = 2;
    public int damage = 5;
    public StrikeOfRejection(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                foreach (BuffEffect be in target.buffEffects)
                {
                    if (be.Name == "Stun")
                    {
                        ShowAbility();
                        AddBuff(target, "Unhealable",buffCooldown);
                        DealDamageEffect(target, damage);
                        break;
                    }
                }
            }


            base.UseEffect();

        }
    }
}
