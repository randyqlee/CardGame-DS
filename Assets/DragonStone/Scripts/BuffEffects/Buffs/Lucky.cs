using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucky : BuffEffect
{
  
	int specialValue = 100;

    public Lucky(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Lucky");
        isBuff = true;
    
    }

    public override void CauseBuffEffect()
    {
        target.chance += specialValue;    
    }

    public override void UndoBuffEffect()
    {
        target.chance -= specialValue;        
    }
}
