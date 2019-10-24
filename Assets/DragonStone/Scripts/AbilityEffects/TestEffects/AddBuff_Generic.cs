using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuff_Generic : CreatureEffect
{
    public int buffCooldown = 2;
    public string buffName1 = "Lucky";
    public string buffName2 = "Retaliate";
    public string buffName3 = "Recovery";

    public string buffName4 = "IncreaseAttack";

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

            //AddBuff(creature, buffName1, buffCooldown);
            AddBuff(creature, buffName2, buffCooldown);
            //AddBuff(creature, buffName3, buffCooldown);
            //AddBuff(creature, buffName4, buffCooldown);
       
            base.UseEffect();
        }
    }
}