using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silence : BuffEffect {
   
    List<CreatureEffect> creatureEffects = new List<CreatureEffect>();    
	
    public Silence(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}

    public override void CauseBuffEffect()
    {        
        this.creatureEffects = target.creatureEffects;
        foreach(CreatureEffect ce in creatureEffects)
        {
            target.canUseAbility = false;
            //ce.UnregisterCooldown();
            ce.UnRegisterEventEffect();

        }
    }

    public override void UndoBuffEffect()
    {
        this.creatureEffects = target.creatureEffects;
        foreach(CreatureEffect ce in creatureEffects)
        {
            target.canUseAbility = true;
            //ce.RegisterCooldown();
            ce.RegisterEventEffect();

        }
    }


    
    
}//DecreaseAttack Method
