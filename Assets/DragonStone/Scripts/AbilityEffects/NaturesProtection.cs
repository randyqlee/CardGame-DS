using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesProtection : CreatureEffect
{
    public int buffCooldown = 1;

    public NaturesProtection(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += PrimaryEffect;     
       creature.e_IsAttacked += SecondaryEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= PrimaryEffect;
        creature.e_IsAttacked -= SecondaryEffect;          
    }

    public void PrimaryEffect (CreatureLogic target)
    {

        if(CanUseAbility())
        {   
            if (ChanceOK(creature.chance))
            {
                if (creature.isPrimaryForm)
                {
                    ShowAbility();
                    target.RemoveRandomBuff();
                    AddBuff(target, "Poison", buffCooldown);                    
                }
     
            }
            base.UseEffect();
        }

    }

    public void SecondaryEffect (CreatureLogic target)
    {

        if(CanUseAbility())
        {   
            if (ChanceOK(creature.chance))
            {
                if (!creature.isPrimaryForm)
                {
                    if (target == creature)
                    {
                        ShowAbility();
                        AddBuff(owner.GetRandomAlly(creature), "Armor",buffCooldown);
                    }
                }
            }
            base.UseEffect();
        }
        
    }

    public override void UseEffect(CreatureLogic target)
    {


    }
}
