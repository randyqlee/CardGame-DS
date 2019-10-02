using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveStance : CreatureEffect
{
    public int buffCooldown = 2;

    public DefensiveStance(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if(CanUseAbility())
        {
            ShowAbility();
            foreach(CreatureLogic cl in owner.AllyList())
            {
                                
                AddBuff(cl, "Defense", buffCooldown);
                AddBuff(cl, "Armor", buffCooldown);
                AddBuff(cl, "Shield", buffCooldown);
            }
                     
            base.UseEffect();
        }
    }
}