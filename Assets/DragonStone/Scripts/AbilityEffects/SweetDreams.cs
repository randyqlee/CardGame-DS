using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class SweetDreams : CreatureEffect{
    public int buffCooldown = 1;
    public SweetDreams(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_AfterAttacking += UseEffect;        
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_AfterAttacking += UseEffect;            
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                target.RemoveBuffsAll();
                AddBuff(target, "AntiBuff", buffCooldown);                
            }
            
            base.UseEffect();
        }
    }
}