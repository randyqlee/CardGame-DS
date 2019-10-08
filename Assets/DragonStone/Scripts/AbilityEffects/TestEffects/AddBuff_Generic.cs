using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuff_Generic : CreatureEffect
{
    public int buffCooldown = 2;
    public string buffName = "Resurrect";

    public AddBuff_Generic(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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

            AddBuff(creature, buffName, buffCooldown);
       
            base.UseEffect();
        }
    }
}