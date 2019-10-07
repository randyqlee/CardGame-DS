using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : BuffEffect {

    int specialValue = 2;

    public Defense(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Defense");
        isBuff = true;
    }

    public override void CauseBuffEffect()
    {        
        target.DamageReductionAdditive += specialValue;      
    }

    public override void UndoBuffEffect()
    {
        target.DamageReductionAdditive -= specialValue;
    }


    
}
