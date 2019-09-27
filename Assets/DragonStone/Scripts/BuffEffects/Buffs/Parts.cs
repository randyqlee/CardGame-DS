using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : BuffEffect
{
  
	
    public Parts(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue = 1) : base (source, target, buffCooldown, specialValue)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Parts");
        isDebuff = true;
    
    }

    public override void CauseBuffEffect()
    {
        target.e_buffApplied += IncreaseDebuffCD;    
    }

    public override void UndoBuffEffect()
    {
        target.e_buffApplied -= IncreaseDebuffCD;       
    }

    public void IncreaseDebuffCD (BuffEffect buff)
    {
        if (buff.isDebuff)
        {
            buff.buffCooldown += specialValue;
            new UpdateBuffCommand(buff).AddToQueue();
        }
    }
}