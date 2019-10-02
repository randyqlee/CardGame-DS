using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairysBlessing : CreatureEffect
{
    public int buffCooldown = 2;
    public int healValue = 12;

    public FairysBlessing(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                cl.Heal(healValue);
                AddBuff(cl, "Immunity", buffCooldown);
                AddBuff(cl, "IncreasedAttack", buffCooldown);
            }
                     
            base.UseEffect();
        }
    }
}