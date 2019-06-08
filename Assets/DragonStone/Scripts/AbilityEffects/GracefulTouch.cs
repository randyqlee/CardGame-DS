using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GracefulTouch : CreatureEffect {

    public int buffCooldown = 1;

    public GracefulTouch(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       if(remainingCooldown <=0)
        Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {                      
        AddBuff(target,"CrippledStrike",buffCooldown);                          
        base.UseEffect();       
    }

}
