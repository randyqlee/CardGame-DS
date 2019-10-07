using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : BuffEffect
{
  
	int specialValue = 1;
    public Parts(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
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

    public void IncreaseDebuffCD (CreatureLogic Target, BuffEffect buff)
    {
        if (buff.isDebuff)
        {
            buff.buffCooldown += specialValue;
            new UpdateBuffCommand(buff).AddToQueue();
        }
    }
}