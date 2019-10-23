using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : CreatureEffect
{
    public int buffCooldown = 2;
    public int reduceCD = 1;

    public Alert(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                foreach(CreatureEffect ce in cl.creatureEffects)
                {
                    ce.remainingCooldown -= reduceCD;
                    ce.UpdateCooldown();
                }
                
                AddBuff(cl, "Evasion", buffCooldown);
            }
                     
            base.UseEffect();
        }
    }
}