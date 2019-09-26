using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : BuffEffect
{
    // Start is called before the first frame update
    public Stealth(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Stealth");
        isBuff = true;    
    }

    public override void CauseBuffEffect()
    {        
        target.canBeAttacked = false;    
    }

    public override void UndoBuffEffect()
    {
        target.canBeAttacked = true;    
    }
}
