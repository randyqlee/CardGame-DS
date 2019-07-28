using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Serenity : CreatureEffect {

    public int buffCooldown = 1;

    public Serenity(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
       creature.e_BeforeAttacking += ShowAbility;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
         creature.e_BeforeAttacking -= ShowAbility;      
    }

    public override void CauseEventEffect()
    {
    //    if(remainingCooldown <=0)
    //     Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {                      
        if(remainingCooldown<=0)
        {
           
            AddBuff(target,"AntiBuff",buffCooldown);
            base.UseEffect();                             
             
        }
        
        
    }

}
