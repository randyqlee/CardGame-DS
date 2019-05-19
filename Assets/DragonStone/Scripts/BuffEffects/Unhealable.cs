using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhealable : BuffEffect {

    int attackModifier = 8;
    
	
    public Unhealable(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}

    public override void CauseBuffEffect()
    {
        target.healFactor = -1;
    }

    public override void UndoBuffEffect()
    {
        target.healFactor = 1;
    }


    
    
}//DecreaseAttack Method
