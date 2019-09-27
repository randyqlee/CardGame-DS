using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endure : BuffEffect
{
    // Start is called before the first frame update
    public Endure(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Endure");
        isBuff = true;    
    }

    public override void CauseBuffEffect()
    {        
        target.hasEndure = true;    
    }

    public override void UndoBuffEffect()
    {
        target.hasEndure = false;    
    }
}
