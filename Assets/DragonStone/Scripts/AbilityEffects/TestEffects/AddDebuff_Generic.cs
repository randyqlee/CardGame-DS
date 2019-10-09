using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDebuff_Generic : CreatureEffect
{
    public int buffCooldown = 2;
    public string debuffName = "Berserk";
   

    public AddDebuff_Generic(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if(CanUseAbility())
        {
            ShowAbility();

            AddBuff(target, debuffName, buffCooldown);
       
            base.UseEffect();
        }
    }
}