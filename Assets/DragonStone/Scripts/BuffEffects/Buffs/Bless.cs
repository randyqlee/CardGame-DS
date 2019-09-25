using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bless : BuffEffect
{
    // Start is called before the first frame update
    public Bless(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Bless");
        isBuff = true;    
    }

    public override void CauseBuffEffect()
    {        
        target.hasBless = true;    
    }

    public override void UndoBuffEffect()
    {
        target.hasBless = false;    
    }
}
