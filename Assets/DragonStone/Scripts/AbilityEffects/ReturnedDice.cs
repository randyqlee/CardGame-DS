using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnedDice : CreatureEffect
{
    public int buffCooldown = 1;
    public int buffCooldown2 = 3;

    public ReturnedDice(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                    ce.remainingCooldown = ce.creatureEffectCooldown;
                    ce.UpdateCooldown();
                }
                AddBuff(cl, "Unlucky", buffCooldown2);
                AddBuff(cl, "Silence", buffCooldown2);

                AddBuff(cl, "Evasion", buffCooldown);
            }

            foreach(CreatureLogic cl in owner.EnemyList())
            {
                foreach(CreatureEffect ce in cl.creatureEffects)
                {
                    ce.remainingCooldown = ce.creatureEffectCooldown;
                    ce.UpdateCooldown();
                }
                AddBuff(cl, "Unlucky", buffCooldown2);
                AddBuff(cl, "Silence", buffCooldown2);
            }

         
            base.UseEffect();
        }
    }
}