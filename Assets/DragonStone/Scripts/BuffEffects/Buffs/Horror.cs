using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horror : BuffEffect
{
  
	
    public Horror(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue = 100) : base (source, target, buffCooldown, specialValue)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Horror");
        isDebuff= true;
    
    }

    public override void CauseBuffEffect()
    {
        target.hasHorror = true;    
    }

    public override void UndoBuffEffect()
    {
        target.hasHorror = false;        
    }
}