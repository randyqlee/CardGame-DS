using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horror : BuffEffect
{
  int specialValue = 100;
	
    public Horror(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
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