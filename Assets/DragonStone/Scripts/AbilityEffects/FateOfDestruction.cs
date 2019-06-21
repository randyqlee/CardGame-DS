using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class FateOfDestruction : CreatureEffect {

    public int buffCooldown = 2;

    public FateOfDestruction(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        
            if(remainingCooldown<=0)
            {
                AddBuff(target,"Poison",buffCooldown);        
                AddBuff(target,"Bomb",buffCooldown);                        
                base.UseEffect();          
            }
            
            
    }
}
