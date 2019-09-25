using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucky : BuffEffect
{
  
	
    public Lucky(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue = 100) : base (source, target, buffCooldown, specialValue)
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
